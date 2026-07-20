import glob
import shutil

# Find existing projects
source = "./MohawkGame2D/"
pattern = "./**/MohawkGame2D/"
dirs = glob.glob(pattern, recursive=True)
print(f"Found {len(dirs)} directory matches for \"{pattern}\".")
# Iterate over each
for directory in dirs:
    # if trying to delete source files due to same name, don't
    if (directory.replace("\\", "/") == source):
        continue
    # List directory
    print(f"Processing \"{directory}\"")
    # Delete folder
    shutil.rmtree(directory)
    # Copy new folder + files
    shutil.copytree(source, directory, dirs_exist_ok=False)
print("Complete")