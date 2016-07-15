using DnD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DnD.Models.DnDAttribute;

namespace DnD.Data
{
    public static class DataSeed
    {
        public static void Seed(IServiceScope serviceScope)
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            if (!context.Attributes.Any() && !context.Races.Any())
            {
                //Create new attributes
                var life = new DnDAttribute() { Name = "Leben", ShortName = "LP", Type = LevelUp.WithCharacterPoints };
                var mana = new DnDAttribute() { Name = "Mana", ShortName = "M", Type = LevelUp.WithCharacterPoints };
                var courage = new DnDAttribute() { Name = "Mut", ShortName = "Mut", Type = LevelUp.WithCharacterPoints };
                var strength = new DnDAttribute() { Name = "Stärke", ShortName = "Str", Type = LevelUp.WithCharacterPoints };
                var intelligence = new DnDAttribute() { Name = "Intelligenz", ShortName = "Int", Type = LevelUp.WithCharacterPoints };
                var charisma = new DnDAttribute() { Name = "Charisma", ShortName = "C", Type = LevelUp.WithCharacterPoints };
                var skill = new DnDAttribute() { Name = "Geschick", ShortName = "G", Type = LevelUp.WithCharacterPoints };
                var protection = new DnDAttribute() { Name = "Schutz", ShortName = "S", Type = LevelUp.Custom };
                var scales = new DnDAttribute() { Name = "Schuppen", ShortName = "Spn", Type = LevelUp.WithAbilityPoints };
                var talons = new DnDAttribute() { Name = "Krallen", ShortName = "K", Type = LevelUp.WithAbilityPoints };

                //Add attributes to db
                context.Attributes.AddRange(life, mana, courage, strength, intelligence, charisma, skill, protection, scales, talons);

                //Create new races
                var human = new Race()
                {
                    Name = "Mensch",
                    Lore = "Menschen sind große kräftige Wesen, die den Elfen ähneln. Doch ihr Körperbau ist wesentlich kräftiger.",
                    Attributes = new List<RaceAttribute>()
                    {
                        new RaceAttribute() { Attribute = courage, Value = 3 },
                        new RaceAttribute() { Attribute = strength, Value = 3 },
                        new RaceAttribute() { Attribute = intelligence, Value = 3 },
                        new RaceAttribute() { Attribute = charisma, Value = 3 },
                        new RaceAttribute() { Attribute = skill, Value = 3 },
                    }
                };

                var elf = new Race()
                {
                    Name = "Elf",
                    Lore = "Elfen sind schlanker als Menschen, man erkennt sie an ihren langen spitzen Ohren, und an ihrer meist sanften Stimme. " +
                        "Vorteil: der Elf besitzt die Fähigkeiten, sich im Wald zu verstecken. Elfen haben fein ausgeprägte Sinne, besonders das Gehör, " +
                        "aber auch Augen und Geruchsinn sind sehr scharf. Unter anderem können Elfen dadurch einen Hinterhalt schon frühzeitig aufspüren. " +
                        "Nachteil: Elfen können die Fertigkeit Eisenrüstung nicht auf Meister erlernen und die Fertigkeit Plattenrüstung gar nicht.",
                    Attributes = new List<RaceAttribute>()
                    {
                        new RaceAttribute() { Attribute = courage, Value = 2 },
                        new RaceAttribute() { Attribute = strength, Value = 2 },
                        new RaceAttribute() { Attribute = intelligence, Value = 3 },
                        new RaceAttribute() { Attribute = charisma, Value = 4 },
                        new RaceAttribute() { Attribute = skill, Value = 4 },
                    }
                };

                var halfelf = new Race()
                {
                    Name = "Halbelf",
                    Lore = "Verbindungen zwischen Menschen und Elfen sind fruchtbar. " +
                        "Die Kinder dieser Verbindungen sind im Allgemeinen etwas schlanker als Menschen, aber kräftiger als Elfen, " +
                        "die Ohren sind nur leicht angespitzt. Die Entwicklung der Halbelfen hängt stark davon ab, bei welchem Volk sie aufgewachsen sind. " +
                        "Bei Menschen aufgewachsene Halbelfen haben wie diese weder besondere Vor- noch Nachteile, Halbelfen, die bei Elfen aufgewachsen sind, " +
                        "haben die Vor- und Nachteile dieser Rasse.",
                    Attributes = new List<RaceAttribute>()
                    {
                        new RaceAttribute() { Attribute = courage, Value = 3 },
                        new RaceAttribute() { Attribute = strength, Value = 2 },
                        new RaceAttribute() { Attribute = intelligence, Value = 3 },
                        new RaceAttribute() { Attribute = charisma, Value = 3 },
                        new RaceAttribute() { Attribute = skill, Value = 4 },
                    }
                };

                var amazon = new Race()
                {
                    Name = "Amazone",
                    Lore = "Im Volk der Amazonen gibt es nur Frauen. Damit das Volk weiterhin besteht, " +
                        "werden von einigen Stämmen die nahe liegenden Dörfer überfallen und die jungen Mädchen entführt. " +
                        "Amazonen sind sehr selbstbewusst und lassen sich nichts von Männern sagen. Vorteil: " +
                        "Hat die Amazone eine Intelligenz von 12 Punkten, kann sie versteckte Fallen entdecken. " +
                        "Greift ein Mann eine Frau an, so hat eine Amazone in der ersten Kampfrunde, so sie in den Kampf eingreifen will, " +
                        "eine Attacke mehr. Nachteil: Beim Umgang mit Männern erleiden Amazonen einen Charismamalus von 1.",
                    Attributes = new List<RaceAttribute>()
                    {
                        new RaceAttribute() { Attribute = courage, Value = 3 },
                        new RaceAttribute() { Attribute = strength, Value = 4 },
                        new RaceAttribute() { Attribute = intelligence, Value = 2 },
                        new RaceAttribute() { Attribute = charisma, Value = 3 },
                        new RaceAttribute() { Attribute = skill, Value = 3 },
                    }
                };

                var druid = new Race()
                {
                    Name = "Druid",
                    Lore = "Druiden sehen wie Menschen aus. Jeder Druide ist praktisch unsterblich, außer er wird im Kampf oder durch einen Unfall getötet. " +
                        "Vorteil: in einem Ritual, das eine Runde dauert und in dieser Zeit jede andere Handlung ausschließt, " +
                        "können Druiden bis zu 5 Manapunkte regenerieren. Dies ist dreimal pro Tag möglich. " +
                        "Druiden können aus dem Blut Verstorbener einen Heiltrank (LP+10) herstellen; dazu braucht es ein passendes Gefäß. " +
                        "Nachteil: Druiden fällt aufgrund ihrer Naturverbundenheit das Zaubern in Städten schwerer, weshalb dort alle Zauber 10% (aufgerundet) teurer sind.",
                    Attributes = new List<RaceAttribute>()
                    {
                        new RaceAttribute() { Attribute = courage, Value = 2 },
                        new RaceAttribute() { Attribute = strength, Value = 1 },
                        new RaceAttribute() { Attribute = intelligence, Value = 5 },
                        new RaceAttribute() { Attribute = charisma, Value = 3 },
                        new RaceAttribute() { Attribute = skill, Value = 4 },
                    }
                };

                var orc = new Race()
                {
                    Name = "Ork",
                    Lore = "Orks sind grüne stinkende Wesen, die wenig von Körperpflege halten. " +
                        "Ihr Körper ist sehr muskulös und mit einer schuppenartigen Haut überzogen. Vorteil: NPC, die gegen einen Ork kämpfen, erleiden einen Mut-Malus von 2. " +
                        "Orks können in einer Runde (in der Zeit sind keine weiteren Aktionen möglich) ein schamanisches Ritual durchführen, " +
                        "mit dem sie für einen halben Tag entweder Mut oder Stärke um 2 Punkte erhöhen können. Nachteil: " +
                        "Orks können nicht den Beruf des Magiers ergreifen und folgende Fertigkeiten nicht bis zum Meistertitel entwickeln: " +
                        "Handeln, Fremdsprachen, Lesen und Schreiben",
                    Attributes = new List<RaceAttribute>()
                    {
                        new RaceAttribute() { Attribute = courage, Value = 5 },
                        new RaceAttribute() { Attribute = strength, Value = 5 },
                        new RaceAttribute() { Attribute = intelligence, Value = 2 },
                        new RaceAttribute() { Attribute = charisma, Value = 1 },
                        new RaceAttribute() { Attribute = skill, Value = 2 },
                    }
                };

                var dwarf = new Race()
                {
                    Name = "Zwerg",
                    Lore = "Zwerge sind kleine und kräftig gebaute Wesen. Sie leben überwiegend in den Höhlen und arbeiten in den Erzminen. " +
                        "Zwerge tragen meist lange Bärte. Vorteil: Aufgrund ihres Verständnisses für Mechanik können Zwerge geheime Vorrichtungen " +
                        "(Fallen, Gänge, Geheimfächer, verborgene Türen etc.) aufspüren. Zwerge können sehr gut im Dunkeln sehen. " +
                        "Nachteil: Zwerge erleiden in Wäldern einen Mut- und Geschicklichkeitsmalus von je 1.",
                    Attributes = new List<RaceAttribute>()
                    {
                        new RaceAttribute() { Attribute = courage, Value = 5 },
                        new RaceAttribute() { Attribute = strength, Value = 4 },
                        new RaceAttribute() { Attribute = intelligence, Value = 2 },
                        new RaceAttribute() { Attribute = charisma, Value = 2 },
                        new RaceAttribute() { Attribute = skill, Value = 2 },
                    }
                };

                var wolf = new Race()
                {
                    Name = "Lykantroph",
                    Lore = "Die Lykantrophe, wie sie sich selbst nennen, haben sich über Generationen hinweg aus den Werwölfen entwickelt. " +
                        "Sie werden etwa 1,70 m bis 2,20 m groß. Im Unterschied zu ihren Vorfahren behalten sie ihre Wolfsgestalt jedoch dauerhaft bei. " +
                        "Vorteil: Lykantrophen können mit Wölfen kommunizieren; " +
                        "Wölfe greifen Lykantrophen nicht von sich aus an (was z.B. Auswirkungen auf manche Reiseereignisse hat). " +
                        "Lykantrophen können ab Level 5 zweimal am Tag reisen. Nachteil: da Lykantrophen nicht sehr beliebt sind, " +
                        "wenden sich 1-3 NPC von ihnen angewidert ab, Informationssuche und dergleichen sind somit erschwert.",
                    Attributes = new List<RaceAttribute>()
                    {
                        new RaceAttribute() { Attribute = courage, Value = 4 },
                        new RaceAttribute() { Attribute = strength, Value = 4 },
                        new RaceAttribute() { Attribute = intelligence, Value = 2 },
                        new RaceAttribute() { Attribute = charisma, Value = 1 },
                        new RaceAttribute() { Attribute = skill, Value = 4 },
                    }
                };

                var dragon = new Race()
                {
                    Name = "Drache",
                    Lore = "Schon seit Jahrtausenden bewohnen die mächtigen Drachen diese Welt. Ja, man kann sagen, sie sind die ältesten Lebewesen, " +
                        "die es auf dieser Welt gibt. Ein Drache ist ca. 5 bis 10 Meter lang, und erreicht eine Flügelspannweite von ca. 20 Meter. " +
                        "Es gibt allerdings verschiedene Drachenrassen. Zum einen die meist schwerfälligen, aber relativ friedlichen Erddrachen. " +
                        "Feuerdrachen hingegen können schnell wütend werden und sind zudem meist sehr neugierig. " +
                        "Die geduldigen, aber meist starrköpfigen Wasserdrachen können dank ihrer Schwimmhäute zwischen den Zehen und " +
                        "dem seitlich abgeflachten Schwanz als einzige Drachen das Schwimmen erlernen. Und dann gibt es noch die verspielten und oft launischen Edeldrachen, " +
                        "die durch ihren leichten Körperbau schnell und gewandt in der Luft manövrieren können. " +
                        "Vorteil: Drachen können fliegen und damit dreimal pro Tag reisen. " +
                        "Nachteil: Drachen können normale Gebäude nicht betreten (man kann aber in das Gebäude hineinrufen).",
                    Attributes = new List<RaceAttribute>()
                    {
                        new RaceAttribute() { Attribute = courage, Value = 3 },
                        new RaceAttribute() { Attribute = strength, Value = 10 },
                        new RaceAttribute() { Attribute = intelligence, Value = 7 },
                        new RaceAttribute() { Attribute = charisma, Value = 2 },
                        new RaceAttribute() { Attribute = skill, Value = 1 },
                        //Dragons have the option to level up scales and talons
                        new RaceAttribute() { Attribute = scales, Value = 0 },
                        new RaceAttribute() { Attribute = talons, Value = 0 },
                        //Dragons have fix initial values for starting life and mana
                        //(other races can individually adjust those at character creation)
                        new RaceAttribute() { Attribute = life, Value = 10 },
                        new RaceAttribute() { Attribute = mana, Value = 10 },

                    }
                };

                var gecko = new Race()
                {
                    Name = "Echsenwesen",
                    Lore = "Die Echsenwesen bewohnen das Dschungelreich von Isua. Sie sind friedliche Wesen, " +
                        "die in Pfahldörfern vom Fischfang und dem Sammeln essbarer Pflanzen leben. " +
                        "Vorteil: Die gespaltene Zunge der Echsenwesen ist ein sehr empfindliches Geruchsorgan. " +
                        "Indem Echsenwesen die Luft \"schmecken\", können sie eine einmal aufgenommene Witterung zielgenau verfolgen, " +
                        "indem sie andere Gerüche der Umgebung ausblenden. Nachteil: Echsenwesen sind wechselwarm, weshalb ihnen Kälte besonders zusetzt. " +
                        "Im Winter bzw. in sehr kalten Gebieten bzw. bei sehr kaltem Wetter " +
                        "(z.B. in den Nordlanden oder im Hochgebirge) erleiden sie deshalb einen Malus von 2 auf ihr Geschick.",
                    Attributes = new List<RaceAttribute>()
                    {
                        new RaceAttribute() { Attribute = courage, Value = 2 },
                        new RaceAttribute() { Attribute = strength, Value = 2 },
                        new RaceAttribute() { Attribute = intelligence, Value = 4 },
                        new RaceAttribute() { Attribute = charisma, Value = 2 },
                        new RaceAttribute() { Attribute = skill, Value = 5 },
                    }
                };

                //Add races to db
                context.Races.AddRange(human, elf, halfelf, amazon, druid, orc, dwarf, wolf, dragon, gecko);

                context.SaveChanges();
            }
        }
    }
}
