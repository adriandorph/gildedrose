using Xunit;
using System.Collections.Generic;
using System.IO;
using System;


namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        Program app;
        public TestAssemblyTests()
        {
            app = new Program()
                          {
                              Items = new List<Item>
                                          {
                new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },//0
                new Item { Name = "Aged Brie", SellIn = 52, Quality = 0 },//1
                new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },//2
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },//3
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },//4
                new Item
                {//5
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Item
                {//6
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 10,
                    Quality = 40
                },
                new Item
                {//7
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 5,
                    Quality = 3
                },
				// this conjured item does work properly
				new Item { //8
                    Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
                                          }
                          };
        }
        
        [Fact]
        public void SellIn_LessOrEqualToZero_QualityDecreaseDoubles()
        {
            //Arrange
            new TestAssemblyTests();
            var initialSellin = app.Items[0].SellIn;

            //Act
            for(int i = 0; i<initialSellin; i++){
                app.UpdateQuality();
            }
            var qualityBefore = app.Items[0].Quality;
            app.UpdateQuality();
            var qualityAfter = app.Items[0].Quality;

            //Assert
            Assert.Equal(2, qualityBefore - qualityAfter);
        }

        [Fact]
        public void ElixirOfTheMoongoose_QualityAndSellin_DecreaseByOne()
        {
            //Arrange
            new TestAssemblyTests();
            var initialElexir = app.Items[2];
            var initQuality = initialElexir.Quality;
            var initSellin = initialElexir.SellIn;

            //Act
            app.UpdateQuality();
            var FinalElexir = app.Items[2];

            //Assert
            Assert.Equal(1, initQuality - FinalElexir.Quality);
            Assert.Equal(1, initSellin - FinalElexir.SellIn);
        }

        
        [Fact]

        public void Quality_of_item_is_never_negative(){
            //Arrange
            new TestAssemblyTests();
            var updatesNeeded = app.Items[0].SellIn + (app.Items[0].Quality - app.Items[0].SellIn) / 2;
            
            //Act
            for(int i = 0; i < updatesNeeded ; i++){
                app.UpdateQuality();
            }
            //Assert
            Assert.Equal(0, app.Items[0].Quality);
        }
        

        //[Fact]
        
         public void Quality_of_item_is_never_greater_than_50(){
            //Arrange
            new TestAssemblyTests();
            //Act
            for(int i = 0; i<6; i++){
                app.UpdateQuality();
            }
            var backstagePass = app.Items[6].Quality;

            for(int i = 0; i<44; i++){
                app.UpdateQuality();
            }
            var brieQuality = app.Items[1].Quality;

            //Assert
            
            Assert.Equal(50, brieQuality);
            Assert.Equal(50, backstagePass);
        }
        
        
        [Fact]
        public void AgedBrie_QualityIncreases() 
        {
            //Arrange
            new TestAssemblyTests();

            //Act
            var InitialQuality = app.Items[1].Quality;
            app.UpdateQuality();
            var FinalQuality = app.Items[1].Quality;
            
            //Assert
            Assert.Equal(1, FinalQuality  - InitialQuality);
        }

        [Fact]
        public void AgedBrie_SellinZero_QualityIncreases() 
        {
            //Arrange
            new TestAssemblyTests();

            //Act
            for(int i = 0; i < 55; i++)
            {
                app.UpdateQuality();
            }
            app.Items[1].Quality = 0;
            var InitialQuality = app.Items[1].Quality;
            app.UpdateQuality();
            var FinalQuality = app.Items[1].Quality;
            
            //Assert
            Assert.Equal(2, FinalQuality  - InitialQuality);
        }

        [Fact]

        public void backStagePass_QualityIncreases(){
            
            new TestAssemblyTests();

        }
        
        //"Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        [Fact]
        public void Sulfuras_QualityStaysEqual()
        {
            //Arrange
            new TestAssemblyTests();
            //Act
            var InitialSulfure = app.Items[3].Quality;
            app.UpdateQuality();
            var FinalSulfure = app.Items[3].Quality;
            
            //Assert
            Assert.Equal(InitialSulfure, FinalSulfure);
        }

        [Fact]
        public void Sulfuras_SellinStaysEqual()
        {
             //Arrange
            new TestAssemblyTests();
            //Act
            var InitialSulfure = app.Items[4].SellIn;
            app.UpdateQuality();
            var FinalSulfure = app.Items[4].SellIn;
            
            //Assert
            Assert.Equal(InitialSulfure, FinalSulfure);
        }



        [Fact]
        public void BackstagePasses_more_than_10_days_quality_increases_by_1(){
            //Arrange
            new TestAssemblyTests();
            var beforeUpdate = app.Items[5].Quality;
            //Act
            app.UpdateQuality();
            
            //Assert
            Assert.Equal(1, app.Items[5].Quality - beforeUpdate);
        }

        [Fact]
        public void BackstagePasses_10_days_or_less_quality_increases_by_2(){
            //Arrange
            new TestAssemblyTests();
            var beforeUpdate = app.Items[6].Quality;
            //Act
            app.UpdateQuality();
            
            //Assert
            Assert.Equal(2, app.Items[6].Quality - beforeUpdate);
        }

        [Fact]
        public void BackstagePasses_5_days_or_less_quality_increases_by_3(){
            //Arrange
            new TestAssemblyTests();
            var beforeUpdate = app.Items[7].Quality;
            //Act
            app.UpdateQuality();
            
            //Assert
            Assert.Equal(3, app.Items[7].Quality - beforeUpdate);
        }

        [Fact]
        public void BackstagePasses_less_than_0_days_quality_drops_to_0(){
            //Arrange
            new TestAssemblyTests();
            //Act
            while(app.Items[7].SellIn >= 0){
                app.UpdateQuality();
            }
            
            //Assert
            Assert.Equal(0, app.Items[7].Quality);
        }
        
        [Fact]
        public static void MainExists()
        {
            var writer = new StringWriter();
            Console.SetOut(writer);
          
            Program.Main(new string[] {});

            var actual = writer.GetStringBuilder().ToString();
            Assert.True(true);
        }

        [Fact]
        public void general_sellIn_less_than_0(){
            //Arrange
            new TestAssemblyTests();

            //Act
            while(app.Items[2].SellIn >= 0)
            {
                app.UpdateQuality();
            }
            //Assert
            Assert.Equal(0, app.Items[2].Quality);
        }

        [Fact]
        public void conjured(){
            //Arrange
            new TestAssemblyTests();
            var qualityBefore = app.Items[8].Quality;

            //Act
            app.UpdateQuality();
            var qualityAfter = app.Items[8].Quality;
            //Assert
            Assert.Equal(2, qualityBefore - qualityAfter);
        }
    }
}