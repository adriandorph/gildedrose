                    //Quality increases by 1
using System;
using System.Collections.Generic;

namespace GildedRose
{
    public class Program
    {
        public IList<Item> Items;

        public IList<bool> conjured;
        public static void Main(string[] args)
        {
            var app = new Program()
                          {
                              Items = new List<Item>
                                          {
                new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 10,
                    Quality = 49
                },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 5,
                    Quality = 49
                },
				// this conjured item does not work properly yet
				new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
                                          }

                          };

            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                for (var j = 0; j < app.Items.Count; j++)
                {
                    Console.WriteLine(app.Items[j].Name + ", " + app.Items[j].SellIn + ", " + app.Items[j].Quality);
                }
                Console.WriteLine("");
                app.UpdateQuality();
            }
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                int qualityChange;
                //GENERAL RULES:
                //Quality of an item is never more than 50 (Only relevant for Aged Brie and backstage pass, since it increases in quality)
                
                //SellIn decreases by one
                Items[i].SellIn -= 1;
                
                switch(Items[i].Name){
                    case "Aged Brie":
                    //Quality increases by 1 (if below 50)
                    qualityChange = AgedBrieUpdate();
                    break;

                    case string a when a.Contains("Sulfuras"):
                    //Quality and Sell in never changes
                    qualityChange = SulfurasUpdate(i);
                    break;

                    case string b when b.Contains("Backstage pass"):
                    //Quality increases by 2 less than 10 days, 3, less than 5 days. 0 when -1 days
                    qualityChange = BackstageUpdate(i);
                    break;

                    case string c when c.Contains("Conjured"):
                    //Quality degrades twice as fast
                    qualityChange = ConjuredUpdate();
                    break;
                    
                    default:
                    qualityChange = DefaultUpdate();
                    break;
                }

                
                
                //General stuff
                if(qualityChange != 0)
                {//If qualityChange is 0 it is Sulfuras
                    if(Items[i].SellIn < 0) qualityChange *= 2;
                    if(Items[i].Quality + qualityChange < 50 && Items[i].Quality + qualityChange >= 0){
                        Items[i].Quality += qualityChange;           
                    }
                }
            }
        }

    private int DefaultUpdate()
    {
        return -1;
    }
    
    private int SulfurasUpdate(int index){
        Items[index].SellIn +=1;
        return 0;
    }

     private int BackstageUpdate(int index){
        int qualityChange = 1;
        int SellIn = Items[index].SellIn;
        if(SellIn < 0) 
        {
            Items[index].Quality = 0;
            return 0;
        }
        else if(SellIn <= 10){
            qualityChange += 1;
            if(SellIn <= 5) qualityChange += 1;
        }
        return qualityChange;
    }
    
    private int ConjuredUpdate(){
        return -2;
    }
    
     private int AgedBrieUpdate(){
        return 1;     
    }
}
}