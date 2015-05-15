using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 07.05.2015
// All managers, shader programs and scenes 
namespace LightingModels
{
    public class Singleton
    {
        // ShaderProgram
        static private ShaderProgram _shaderProgram = null;
        static public ShaderProgram ShaderProgram { get { return _shaderProgram; } }

        // OpenTKManager
        static private OpenTKManager _openTKManager = null;
        static public OpenTKManager OpenTKManager { get { return _openTKManager; } }

        // Scene1
        static private Scene1 _scene1 = null;
        static public Scene1 Scene1 { get { return _scene1; } }

        // initialize all
        private void Create()
        {
            _shaderProgram = new ShaderProgram();
            _openTKManager = new OpenTKManager();
            _scene1 = new Scene1();
        }

        public void Init()
        {
            Create();
        }
    }
}
