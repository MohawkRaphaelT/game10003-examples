import glob
import shutil
import sys

# See what user wants deleted
pattern = sys.argv[1]
# Match some pattern
dir_matches = glob.glob(pattern, recursive=True)
print(f"Found {len(dir_matches)} directory matches for \"{pattern}\".")
# Iterate over each
for dir_match in dir_matches:
    # List directory
    print(f"Deleting \"{dir_match}\"")
    # Delete folder
    shutil.rmtree(dir_match)
print("Complete")