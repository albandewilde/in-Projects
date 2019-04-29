using System;

namespace inProject.ExtractOnlineTomlData
{
    // Given a Google Drive, DropBox or OneDrive url, we save the file in .toml in our data base
    public class ExtractOnlineTomlData
    {
        private string tomlFileName;    // string the file is named
        private dynamic requiredFieldAndTypes;    // toml file with each table, key and their value type

        public ExtractOnlineTomlData(string toml_file_name, dynamic field_and_types)
        {
            this.tomlFileName = toml_file_name;
            this.requiredFieldAndTypes = field_and_types;
        }

        // Given the url repositorie, we return the toml as a dynamic
        public dynamic SearchOnRepositorie(string url)
        {
            throw new NotImplementedException();
        }

        // Ckeck if field in the given toml file match to the expected required field and types.
        public bool MatchRequiredFieldAndTypes()
        {
            throw new NotImplementedException();
        }
    }
}
