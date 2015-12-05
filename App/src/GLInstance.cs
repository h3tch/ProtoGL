﻿using OpenTK;
using System;
using System.Linq;
using System.Reflection;

namespace App
{
    class GLInstance : GLObject
    {
        public object instance = null;
        private MethodInfo update = null;
        private MethodInfo endpass = null;
        private MethodInfo delete = null;

        /// <summary>
        /// Create class instance of a C# class compiled through GLCSharp.
        /// </summary>
        /// <param name="dir">Directory of the tech-file.</param>
        /// <param name="name">Name used to identify the object.</param>
        /// <param name="annotation">Annotation used for special initialization.</param>
        /// <param name="text">Text block specifying the object commands.</param>
        /// <param name="classes">Collection of scene objects.</param>
        public GLInstance(string dir, string name, string annotation, string text, Dict<GLObject> classes)
            : base(name, annotation)
        {
            var err = new CompileException($"instance '{name}'");

            // PARSE TEXT TO COMMANDS
            var body = new Commands(text, err);

            // GET CLASS COMMAND
            var cmds = body["class"].ToList();
            if (cmds.Count == 0)
                throw err.Add("Instance must specify a 'class' command " +
                    "(e.g., class csharp_name class_name).");
            var cmd = cmds.First();

            // FIND CSHARP CLASS DEFINITION
            var csharp = classes.GetValue<GLCsharp>(cmd.args[0]);
            if (csharp == null)
            {
                err.Add($"Could not find csharp code '{cmd.args[0]}' of command '{cmd.cmd} "
                    + string.Join(" ", cmd.args) + "'.");
                return;
            }

            // INSTANTIATE CSHARP CLASS
            instance = csharp.CreateInstance(cmd.args[1], name, body.ToDict(), err);
            if (err.HasErrors())
                throw err;

            // get Bind method from main class instance
            update = instance.GetType().GetMethod("Update", new [] {
                typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)
            });

            // get Unbind method from main class instance
            endpass = instance.GetType().GetMethod("EndPass", new [] { typeof(int) });

            // get Delete method from main class instance
            delete = instance.GetType().GetMethod("Delete");

            // get all public methods and check whether
            // they can be used as event handlers for glControl
            GLReference reference = classes.GetValue<GLReference>(GraphicControl.nullname);
            GraphicControl glControl = (GraphicControl)reference.reference;
            var methods = instance.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach (var method in methods)
            {
                var info = glControl.GetType().GetEvent(method.Name);
                if (info != null)
                {
                    var csmethod = Delegate.CreateDelegate(info.EventHandlerType, instance, method.Name);
                    info.AddEventHandler(glControl, csmethod);
                }
            }
        }

        public void Update(int program, int width, int height, int widthTex, int heightTex)
        {
            update?.Invoke(instance, new object[] { program, width, height, widthTex, heightTex });
        }

        public void EndPass(int program)
        {
            endpass?.Invoke(instance, new object[] { program });
        }

        public override void Delete()
        {
            delete?.Invoke(instance, null);
        }
    }
}
