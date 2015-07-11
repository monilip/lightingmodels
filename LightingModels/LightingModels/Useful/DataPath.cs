using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightingModels
{
    public class DataPath
    {
        private static string texturesPath = "textures/";
        private static string materialsPath = "materials/";
        private static string modelsPath = "models/fromBlender/";


        public static string TexturesPath { get { return texturesPath; } }
        public static string MaterialsPath { get { return materialsPath; } }
        public static string ModelsPath { get { return modelsPath; } }
    }
}
