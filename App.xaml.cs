namespace jvargasS5
{
    public partial class App : Application
    {
        public static PersonRepository personRepo { get; set; }
        public App(PersonRepository personRepository)
        {
            InitializeComponent();

            MainPage = new Viuws.Vpersona();
            personRepo = personRepository;
        }
    }
}
