namespace Backend
{
    public class ConcreteStatService : IStatService
    {
        public List<Tuple<string, long>> GetCountryPopulations()
        {
            // Pretend this calls a REST API somewhere
            return new List<Tuple<string, long>>
            {
		        Tuple.Create<string, long>("India",1182105000),
		        Tuple.Create<string, long>("United Kingdom",62026962),
		        Tuple.Create<string, long>("Chile",17094270),
		        Tuple.Create<string, long>("Mali",15370000),
		        Tuple.Create<string, long>("Greece",11305118),
		        Tuple.Create<string, long>("Armenia",3249482),
		        Tuple.Create<string, long>("Slovenia",2046976),
		        Tuple.Create<string, long>("Saint Vincent and the Grenadines",109284),
		        Tuple.Create<string, long>("Bhutan",695822),
		        Tuple.Create<string, long>("Aruba (Netherlands)",101484),
		        Tuple.Create<string, long>("Maldives",319738),
		        Tuple.Create<string, long>("Mayotte (France)",202000),
		        Tuple.Create<string, long>("Vietnam",86932500),
		        Tuple.Create<string, long>("Germany",81802257),
		        Tuple.Create<string, long>("Botswana",2029307),
		        Tuple.Create<string, long>("Togo",6191155),
		        Tuple.Create<string, long>("Luxembourg",502066),
		        Tuple.Create<string, long>("U.S. Virgin Islands (US)",106267),
		        Tuple.Create<string, long>("Belarus",9480178),
		        Tuple.Create<string, long>("Myanmar",59780000),
		        Tuple.Create<string, long>("Mauritania",3217383),
		        Tuple.Create<string, long>("Malaysia",28334135),
		        Tuple.Create<string, long>("Dominican Republic",9884371),
		        Tuple.Create<string, long>("New Caledonia (France)",248000),
		        Tuple.Create<string, long>("Slovakia",5424925),
		        Tuple.Create<string, long>("Kyrgyzstan",5418300),
		        Tuple.Create<string, long>("Lithuania",3329039),
		        Tuple.Create<string, long>("United States of America",309349689)
            };
        }


        public Task<List<Tuple<string, long>>> GetCountryPopulationsAsync()
        {
            return Task.FromResult(GetCountryPopulations());
        }
    }
}
