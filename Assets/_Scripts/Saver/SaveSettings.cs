namespace Saver
{
    using System.Collections.Generic;

    public class SaveSettings
    {
        public Dictionary<ESaveData, string> pathToSave = new Dictionary<ESaveData, string>()
        {
            { ESaveData.Products , "Products.json"}
        };
    }
}

