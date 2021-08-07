namespace Project
{
    public class Student
    {
        public int StudentNumber {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public float Average { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + Average;
        }
    }
}