using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2023
{
    internal class CardRank {
        public string Card { get; set; } = "";
        public int Rank { get; set; } = 0;
    }

    internal class HandBid {
        public string OgHand { get; set; } = "";

        public string Hand { get; set; } = "";



        public int Bid { get; set; } = 0;

        public Dictionary<string, int> Cards { get; set; } = new Dictionary<string, int>();

        public List<CardRank> CardRanks { get; set; } = new List<CardRank>();

        public int RankScore { get; set; } = 0;

        public HandType HandType { get; set; } = HandType.HighCard;



        public Dictionary<string, int> CardsWildcard { get; set; } = new Dictionary<string, int>();

        public List<CardRank> CardRanksWildcard { get; set; } = new List<CardRank>();

        public string HandWildcard { get; set; } = "";

        public HandType HandTypeWildcard { get; set; } = HandType.HighCard;

        public int RankScoreWildcard { get; set; } = 0;
    }

    enum HandType {
        HighCard = 1,
        OnePair = 2,
        TwoPair = 3,
        ThreeOfAKind = 4,
        FullHouse = 5,
        FourOfAKind = 6,
        FiveOfAKind = 7
    }

    internal class Day07App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2023/Day07/input.txt");

        public void Execute() {
            var hands = new List<HandBid>();

            Input.ForEach(handBid => {
                var line = handBid.Split(' ');
                var hb = new HandBid() {
                    OgHand = line[0].Trim(),

                    Hand = new string(line[0].Trim().OrderByDescending(c => GetCardRank(c.ToString()).Value).ToArray()),

                    Bid = Convert.ToInt32(line[1].Trim())
                };

                PrepHandInfo(ref hb, hb.OgHand);
                hands.Add(hb);
            });

            EvaluateHands(hands, false);


            // Part 1
            var orderedHands = hands
                .OrderBy(m => m.HandType)
                .ThenBy(m => m.CardRanks[0].Rank)
                .ThenBy(m => m.CardRanks.Count > 0 ? m.CardRanks[1].Rank : 0)
                .ThenBy(m => m.CardRanks.Count > 1 ? m.CardRanks[2].Rank : 0)
                .ThenBy(m => m.CardRanks.Count > 2 ? m.CardRanks[3].Rank : 0)
                .ThenBy(m => m.CardRanks.Count > 3 ? m.CardRanks[4].Rank : 0)
                .ToList();

            var outputHands = orderedHands.Select(m => m.OgHand).ToList();

            long total = 0;
            for (var i = 1; i <= orderedHands.Count(); i++) {
                total += orderedHands[i - 1].Bid * i;
            }

            Console.WriteLine(total);

            // Part 2
            var orderedHandsWildcard = hands
                .OrderBy(m => m.HandTypeWildcard)
                .ThenBy(m => m.CardRanksWildcard[0].Rank)
                .ThenBy(m => m.CardRanksWildcard.Count > 0 ? m.CardRanksWildcard[1].Rank : 0)
                .ThenBy(m => m.CardRanksWildcard.Count > 1 ? m.CardRanksWildcard[2].Rank : 0)
                .ThenBy(m => m.CardRanksWildcard.Count > 2 ? m.CardRanksWildcard[3].Rank : 0)
                .ThenBy(m => m.CardRanksWildcard.Count > 3 ? m.CardRanksWildcard[4].Rank : 0)
                .ToList();

            var outputHandsWildcard = orderedHandsWildcard.Select(m => $"{m.HandTypeWildcard.ToString()} - {m.OgHand} - {m.HandWildcard}").ToList();

            long totalWildcard = 0;
            for (var i = 1; i <= orderedHandsWildcard.Count(); i++) {
                totalWildcard += orderedHandsWildcard[i - 1].Bid * i;
            }

            Console.WriteLine(totalWildcard);

        }

        public void EvaluateHands(List<HandBid> hands, bool usePokerHands = true) {
            // Base scores for each hand type
            var baseScores = new Dictionary<HandType, int>
            {
                { HandType.HighCard, 1000 },
                { HandType.OnePair, 2000 },
                { HandType.TwoPair, 3000 },
                { HandType.ThreeOfAKind, 4000 },
                { HandType.FullHouse, 5000 },
                { HandType.FourOfAKind, 6000 },
                { HandType.FiveOfAKind, 7000 }
            };

            foreach (var hand in hands) {
                int handScore = baseScores[hand.HandType];
                int handScoreWildcard = baseScores[hand.HandTypeWildcard];

                if (usePokerHands) {
                    // Evaluate by ACTUAL poker hands
                    var orderedCards = hand.Cards.OrderByDescending(c => c.Value).ThenByDescending(c => GetCardRank(c.Key)).ToList();

                    int multiplier = 10000;
                    foreach (var card in orderedCards) {
                        handScore += (GetCardRank(card.Key).Value * card.Value) * multiplier;
                        multiplier /= 10;
                    }
                } else {
                    // Evaluate by card order
                    //int multiplier = 10000;
                    foreach (var card in hand.OgHand.ToCharArray()) {
                        handScore += GetCardRank(card.ToString()).Value; // * multiplier;
                        handScoreWildcard += GetCardRankWildcard(card.ToString()).Value;
                        //multiplier /= 10;
                    }                    
                }

                hand.RankScore = handScore;
                hand.RankScoreWildcard = handScoreWildcard;
            }
        }

        private void PrepHandInfo(ref HandBid handInfo, string hand) {
            var handCards = new Dictionary<string, int>();
            var cardRanks = new List<CardRank>();
            var cardRanksWildcard = new List<CardRank>();

            hand.ToCharArray().ToList().ForEach(card => {
                var cardRank = GetCardRank(card.ToString());
                var cardRankWildcard = GetCardRankWildcard(card.ToString());

                if (!handCards.ContainsKey(card.ToString())) {
                    handCards.Add(card.ToString(), 1);
                } else {
                    handCards[card.ToString()]++;
                }

                cardRanks.Add(new CardRank() { 
                    Card = card.ToString(),
                    Rank = cardRank.Value
                });

                cardRanksWildcard.Add(new CardRank() {
                    Card = card.ToString(),
                    Rank = cardRankWildcard.Value
                });
            });
            handInfo.Cards = handCards;
            handInfo.CardRanks = cardRanks;
            handInfo.CardRanksWildcard = cardRanksWildcard;

            // We need to see which card we have the most of
            handInfo.HandWildcard = handInfo.OgHand;
            if (handInfo.OgHand.IndexOf("J") >= 0) {
                var nonJokers = handCards.ToList().Where(m => m.Key != "J");

                if (nonJokers.Where(m => m.Key != "J").Count() > 0) {
                    var mostCard = nonJokers.OrderByDescending(m => m.Value).ThenByDescending(m => GetCardRankWildcard(m.Key).Value).First();

                    handInfo.HandWildcard = handInfo.OgHand.Replace("J", mostCard.Key);
                }

            }

            var handCardsWildcard = new Dictionary<string, int>();
            handInfo.HandWildcard.ToCharArray().ToList().ForEach(card => {
                var cardRankWildcard = GetCardRankWildcard(card.ToString());
                if (!handCardsWildcard.ContainsKey(card.ToString())) {
                    handCardsWildcard.Add(card.ToString(), 1);
                } else {
                    handCardsWildcard[card.ToString()]++;
                }
            });
            handInfo.CardsWildcard = handCardsWildcard;

            // Get the hand type
            handInfo.HandType = GetHandType(handCards);
            handInfo.HandTypeWildcard = GetHandType(handCardsWildcard);
        }

        private HandType GetHandType(Dictionary<string, int> handCards) {
            var handType = HandType.HighCard;

            if (handCards.Count == 4) {
                // One pair
                handType = HandType.OnePair;

            } else if (handCards.Count == 3) {
                if (handCards.Any(m => m.Value == 3)) {
                    // Three of a kind
                    handType = HandType.ThreeOfAKind;

                } else {
                    // two pair
                    handType = HandType.TwoPair;

                }

            } else if (handCards.Count == 2) {
                if (handCards.Any(m => m.Value == 3)) {
                    // Full house
                    handType = HandType.FullHouse;

                } else {
                    // Four of a kind
                    handType = HandType.FourOfAKind;
                }

            } else if (handCards.Count == 1) {
                // Five of a kind
                handType = HandType.FiveOfAKind;
            }

            return handType;
        }

        private KeyValuePair<string, int> GetCardRank(string card) {
            var ranks = new Dictionary<string, int>() {
                { "A", 14 },
                { "K", 13 },
                { "Q", 12 },
                { "J", 11 },
                { "T", 10 },
                { "9", 9 },
                { "8", 8 },
                { "7", 7 },
                { "6", 6 },
                { "5", 5 },
                { "4", 4 },
                { "3", 3 },
                { "2", 2 }
            };

            var rankList = ranks.ToList();

            return rankList.First(m => m.Key == card);
        }

        private KeyValuePair<string, int> GetCardRankWildcard(string card) {
            var ranks = new Dictionary<string, int>() {
                { "A", 14 },
                { "K", 13 },
                { "Q", 12 },
                { "T", 10 },
                { "9", 9 },
                { "8", 8 },
                { "7", 7 },
                { "6", 6 },
                { "5", 5 },
                { "4", 4 },
                { "3", 3 },
                { "2", 2 },
                { "J", 1 }
            };

            var rankList = ranks.ToList();

            return rankList.First(m => m.Key == card);
        }

    }
}
