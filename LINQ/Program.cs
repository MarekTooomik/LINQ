namespace LINQ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("LINQ");

            WhereLINQ();

            PeopleByAge();

            ThenByLINQ();

            ThenByDescendingLINQ();

            ToLookupLINQ();

            JoinLINQ();

            GroupJoinLINQ();

            SelectLINQ();

            AllAndAnyLINQ();

            ContainsLINQ();

            AggregateLINQ();

            AvarageLINQ();

            CountLINQ();

            MaxLINQ();

            SumLINQ();

            UnionLINQ();

            Skip();

            SkipWhile();

            Take();

            TakeWhile();
        }


        public static void WhereLINQ()
        {
            var filteredResult = PeopleList.people.Where((s, i) =>
            {
                if (i % 2 == 0)
                {
                    return true;
                }
                return false;
            });

            foreach (var people in filteredResult)
            {
                Console.WriteLine(people.Name);
            }
        }

        public static void PeopleByAge()
        {
            Console.WriteLine("Vanuse järgi selekteerimine");

            var peopleByAge = PeopleList.people
                .Where(s => s.Age > 14 && s.Age < 20);

            foreach (var people in peopleByAge)
            {
                Console.WriteLine(people.Age + " " + people.Name);
            }
        }

        public static void ThenByLINQ()
        {
            Console.WriteLine("ThenBy ja ThenByDescending järgi reastamine");

            var thenByResult = PeopleList.people
                .OrderBy(x => x.Name)
                .ThenBy(y => y.Age);

            Console.WriteLine("ThenBy järgi");
            foreach (var people in thenByResult)
            {
                Console.WriteLine(people.Name + " " + people.Age);
            }
        }

        public static void ThenByDescendingLINQ()
        {
            Console.WriteLine("ThenByDescending järgi reastamine");

            var thenByDescending = PeopleList.people
                .OrderBy(x => x.Name)
                .ThenByDescending(y => y.Age);

            foreach (var people in thenByDescending)
            {
                Console.WriteLine(people.Name + " " + people.Age);
            }
        }

        public static void ToLookupLINQ()
        {
            Console.WriteLine("ToLookup järgi reastamine");

            var toLookup = PeopleList.people
                .ToLookup(x => x.Age);

            foreach (var people in toLookup)
            {
                Console.WriteLine("Age group " + people.Key);

                foreach (People p in people)
                {
                    Console.WriteLine("Person name {0}", p.Name);
                }
            }
        }

        public static void JoinLINQ()
        {
            Console.WriteLine("InnerJoin in LINQ");

            var innerJoin = PeopleList.people.Join(
                GenderList.genderList,
                humans => humans.GenderId,
                gender => gender.Id,
                (humans, gender) => new
                {
                    Name = humans.Name,
                    Sex = gender.Sex
                });

            foreach (var obj in innerJoin)
            {
                Console.WriteLine("{0} - {1}", obj.Name, obj.Sex);
            }
        }

        public static void GroupJoinLINQ()
        {
            Console.WriteLine("Group Join LINQ");

            var groupJoin = GenderList.genderList
                .GroupJoin(PeopleList.people,
                p => p.Id,
                g => g.GenderId,
                (p, peopleGroup) => new
                {
                    Humans = peopleGroup,
                    GenderFullName = p.Sex
                });

            foreach (var item in groupJoin)
            {
                Console.WriteLine(item.GenderFullName);

                foreach (var name in item.Humans)
                {
                    Console.WriteLine(name.Name);
                }
            }
        }

        public static void SelectLINQ()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("Select in LINQ");

            var selectResult = PeopleList.people
                .Select(x => new
                {
                    Name = x.Name,
                    Age = x.Age
                });

            foreach (var item in selectResult)
            {
                Console.WriteLine("Human name: {0}, Age: {1}", item.Name, item.Age);
            }
        }

        public static void AllAndAnyLINQ()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("All LINQ");

            bool areAllPeopleTeenagers = PeopleList.people
                .All(x => x.Age > 12);
            //vastus tuleb true
            Console.WriteLine(areAllPeopleTeenagers);

            Console.WriteLine("Any LINQ");
            bool isAnyPersonTeenAger = PeopleList.people
                .Any(x => x.Age < 12);
            //kasv]i [ks andmerida vastab tingimusele
            Console.WriteLine(isAnyPersonTeenAger);
        }

        public static void ContainsLINQ()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Contains LINQ");

            //pärib, kas on number 10 numbrite nimekirjas olemas
            bool result = NumberList.numberList.Contains(10);

            Console.WriteLine(result);
        }

        public static void AggregateLINQ()
        {
            string commaSeparatedPersonNames = PeopleList.people
                .Aggregate<People, string>(
                "People names: ",
                (str, y) => str += y.Name + ", "
                );

            Console.WriteLine(commaSeparatedPersonNames);
        }

        public static void AvarageLINQ()
        {
            Console.WriteLine("Avarage LINQ");

            var avarageResult = PeopleList.people
                .Average(x => x.Age);

            Console.WriteLine(avarageResult);
        }

        public static void CountLINQ()
        {
            var totalPersons = PeopleList.people.Count();

            Console.WriteLine("Inimesi on kokku: " + totalPersons);

            var adultPersons = PeopleList.people.Count(x => x.Age >= 18);

            Console.WriteLine("Täisealisi inimesi on : " + adultPersons);
        }

        public static void MaxLINQ()
        {
            Console.WriteLine("Max LINQ");

            var oldestPerson = PeopleList.people
                .Max(x => x.Age);

            Console.WriteLine("Oldest person age is " + oldestPerson);
        }

        public static void SumLINQ()
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine("Sum LINQ");

            var sumAge = PeopleList.people.Sum(x => x.Age);

            Console.WriteLine(sumAge);

            Console.WriteLine("Täisealiste isikute koondvanus");

            var sumAdult = 0;

            var numAdults = PeopleList.people.Sum(x =>
            {
                if (x.Age >= 18)
                {

                    sumAdult += x.Age;
                    return 1;
                }
                else
                {
                    return 0;
                }
            });

            Console.WriteLine("Täiskasvanud isikute arv " + numAdults);
            Console.WriteLine("Täiskasvanute koondvanuse tulemus " + sumAdult);
        }

        public static void UnionLINQ()
        {
            Console.WriteLine("\n\n----------------Union------------");
            //kõik kordused teeb üheks e topelt ei kuva

            IList<string> list1 = new List<string>() { "car", "bike", "truck", "FOOT" };
            IList<string> list2 = new List<string>() { "Car", "BIKE", "truck", "FOOT" };

            var result = list1.Union(list2);

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        public static void Skip()
        {
            Console.BackgroundColor = ConsoleColor.Black;

            Console.WriteLine("\n\n----------------Skip------------");

            var skip = PeopleList.people.Skip(3);

            foreach (var item in skip)
            {
                Console.WriteLine(item.Id);
            }
        }

        public static void SkipWhile()
        {
            Console.WriteLine("\n\n----------------SkipWhile------------");
            //jätab nimekirjast vahele nii kaua, kuni leiab sobiva kattuvuse
            //ja peale seda näitab kogu nimekirja

            var skipWhile = PeopleList.people
                .SkipWhile(x => x.Age > 18);

            foreach (var item in skipWhile)
            {
                Console.WriteLine(item.Name + " " + item.Age);
            }
        }

        public static void Take()
        {
            Console.WriteLine("\n\n----------------Take------------");
            //n'itab kolme esimest

            var take = PeopleList.people.Take(3);

            foreach (var item in take)
            {
                Console.WriteLine(item.Id + " " + item.Name);
            }
        }

        public static void TakeWhile()
        {
            Console.WriteLine("\n\n----------------TakeWhile------------");

            var takeWhile = PeopleList.people.TakeWhile(x => x.Age > 18);

            foreach (var item in takeWhile)
            {
                Console.WriteLine(item.Id + " " + item.Name);
            }
        }
    }
}