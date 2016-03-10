//###################################################################################
// Externals setup
//      external files archives needed to build artifacts

string folder_externals_extensive = System.IO.Path.Combine(".", "external", "binaries-xtensive");
string folder_externals_minimal   = System.IO.Path.Combine(".", "external", "binaries-minimal");

Dictionary<string, string> files_external = new Dictionary<string, string>();

Task("Setup-Externals-HolisticWare")
    .Does
	(
		() =>
		{
			foreach (KeyValuePair<string, string> kvp in files_external)
			{
				Information(kvp.Key + " = " + kvp.Value);
                //DownloadFile(kvp.Value, kvp.Key);
			}
		}
	);


//###################################################################################
// Library setup
//      project names needed to build library (normal or bindings)

List<string>            library_projectnames = new List<string>();
List<string> 			library_project_outputfiles = new List<string>();
List<OutputFileCopy> 	library_output_files_to_copy = new List<OutputFileCopy>();


Task("SetupLibraryHolisticWare")
    .IsDependentOn("Setup-Externals-HolisticWare")
    .Does
	(
		() =>
		{
			foreach (string s in library_projectnames)
			{
				Information("output_file = " + output_file);
				string output_file = System.IO.Path.Combine(".", "source", "bin" ,"Release", s + ".dll"); 
				OutputFileCopy ofc = new OutputFileCopy()
				{
					FromFile = output_file
				};
				
				library_output_files_to_copy.Add(ofc);
			}
		}
	)
    ;



//###################################################################################
//  injecting setup steps - personal

Task("Default").IsDependentOn("SetupLibraryHolisticWare");

