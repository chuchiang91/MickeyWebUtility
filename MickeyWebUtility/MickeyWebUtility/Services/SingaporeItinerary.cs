using MickeyWebUtility.Models;

namespace MickeyWebUtility.Services
{
    public class SingaporeItinerary
    {
        public Dictionary<string, List<Itinerary>> GetSingaporeItinerary()
        {
            return new Dictionary<string, List<Itinerary>>
            {
                ["day1"] = new List<Itinerary>
                {
                    new Itinerary { Time = "08:15", Activity = "Leave house", Icon = "home" },
                    new Itinerary { Time = "09:45", Activity = "Reach airport (2 hours before flight)", Icon = "plane" },
                    new Itinerary { Time = "11:45 - 13:00", Activity = "Flight", Icon = "plane" },
                    new Itinerary { Time = "13:00 - 15:00", Activity = "Song Fa Bkt Changi Airport - Jewel B2-278 + collect SIM card", Icon = "utensils", Location = "Jewel Changi Airport" },
                    new Itinerary { Time = "15:00 - 17:00", Activity = "Check in + Rest at Hotel Jen", Icon = "hotel", Location = "Tanglin" },
                    new Itinerary { Time = "17:00 - 19:00", Activity = "Funan Mall (敏丁拌饭 01-13/正南柒百米线)", Icon = "coffee", Location = "Funan Mall" },
                    new Itinerary { Time = "19:00 - 21:15", Activity = "Noodle Villa + Nau Tea", Icon = "utensils", Location = "313 Somerset B3-10, 111 Somerset 01-K5" },
                    new Itinerary { Time = "21:15", Activity = "Back to hotel", Icon = "hotel" }
                },
                ["day2"] = new List<Itinerary>
                {
                    new Itinerary { Time = "10:00 - 11:20", Activity = "Maxwell Food Centre (天天鸡饭)", Icon = "utensils", Location = "Maxwell Food Centre" },
                    new Itinerary { Time = "11:20 - 13:45", Activity = "Rest at Hotel Jen", Icon = "hotel", Location = "Tanglin" },
                    new Itinerary { Time = "13:45 - 14:45", Activity = "Queic by Olivia (cheesecake)", Icon = "coffee", Location = "Kreta Ayer Rd" },
                    new Itinerary { Time = "14:45 - 16:45", Activity = "Art Science Museum", Icon = "camera" },
                    new Itinerary { Time = "16:45 - 19:00", Activity = "Dumpling Darlings + Whiteshades Ice Cream", Icon = "utensils", Location = "Amoy Street, Teluk Ayer" },
                    new Itinerary { Time = "19:00 - 20:15", Activity = "Gardens by the Bay (Light show at 19:45)", Icon = "sun" },
                    new Itinerary { Time = "20:15 - 21:20", Activity = "Marina Bay Sands (Spectra show at 21:00) + Heytea", Icon = "sun", Location = "Marina Bay Sands #01-73/74" },
                    new Itinerary { Time = "21:20", Activity = "Back to hotel", Icon = "hotel" }
                },
                ["day3"] = new List<Itinerary>
                {
                    new Itinerary { Time = "08:30", Activity = "Leave hotel", Icon = "hotel" },
                    new Itinerary { Time = "09:30 - 11:00", Activity = "Brunch at Tanglin Cookhouse", Icon = "coffee", Location = "Tanglin Mall 01-016" },
                    new Itinerary { Time = "11:10 - 13:50", Activity = "Changi Airport + Shopping", Icon = "plane" },
                    new Itinerary { Time = "15:10", Activity = "Go to gate", Icon = "plane" },
                    new Itinerary { Time = "16:10 - 17:30", Activity = "Flight", Icon = "plane" }
                }
            };
        }
    }
}
