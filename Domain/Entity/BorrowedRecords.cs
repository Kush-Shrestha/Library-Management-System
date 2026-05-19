namespace LibraryCrud.Domain.Entity
{
    public class BorrowedRecords
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public int MemberID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }# Remove the cached .vs folder from git tracking
git rm -r --cached .vs/

# Add the .gitignore
git add .gitignore

# Add all other files
git add .

# Make your first commit
git commit -m "Initial commit: Add LibraryCrud project"

# Rename branch to main and push
git branch -M main
git push -u origin main

}
