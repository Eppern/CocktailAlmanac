/*SEED THE INGREDIENT DATABASE*/

DELETE FROM RECIPE

GO

DELETE FROM INGREDIENT

GO

Declare @User nvarchar(128)
Declare @ml  int

set @User = (select TOP 1 Id from AspNetUsers)
set @ml  = (select MeasurementUnitId from MEASUREMENT_UNIT where ShortName = 'ml')

INSERT INTO INGREDIENT VALUES 
('Vodka', 'A distilled beverage composed primarily of water and ethanol, sometimes with traces of impurities and flavorings. Traditionally, vodka is made by the distillation of fermented cereal grains or potatoes, though some modern brands use other substances, such as fruits or sugar.', GETDATE(), GETDATE(), @User, @User, 40, @ml, 'http://pngimg.com/upload/bottle_PNG2093.png'),
('Gin', 'Gin is a spirit which derives its predominant flavour from juniper berries (Juniperus communis). From its earliest origins in the Middle Ages, gin has evolved over the course of a millennium from a herbal medicine to an object of commerce in the spirits industry.', GETDATE(), GETDATE(), @User, @User, 42, @ml, 'https://blackbuttondistilling.com/wp-content/uploads/2015/08/New-Gin-Bottle.png'),
('Whisky', 'Whisky or whiskey[1] is a type of distilled alcoholic beverage made from fermented grain mash. Various grains (which may be malted) are used for different varieties, including barley, corn (maize), rye, and wheat. Whisky is typically aged in wooden casks, generally made of charred white oak.', GETDATE(), GETDATE(), @User, @User, 38, @ml, 'https://upload.wikimedia.org/wikipedia/commons/7/74/Glass_of_Bell''s.png'),
('White Rum', 'Rum is a distilled alcoholic beverage made from sugarcane byproducts, such as molasses, or directly from sugarcane juice, by a process of fermentation and distillation. The distillate, a clear liquid, is then usually aged in oak barrels.', GETDATE(), GETDATE(), @User, @User, 37.5, @ml, 'http://havana-club.com/sites/default/files/havana_club_anejo_3_anos_5.png'),
('Dark Rum', 'Rum is a distilled alcoholic beverage made from sugarcane byproducts, such as molasses, or directly from sugarcane juice, by a process of fermentation and distillation. The distillate, a clear liquid, is then usually aged in oak barrels.', GETDATE(), GETDATE(), @User, @User, 37.5, @ml, 'http://www.ahriiserum.dk/download/A-H-RIISE-Rum-lille_XO_PNG.png'),
('Kahlua', 'Kahlúa is a coffee-flavored sugar-based liqueur from Mexico. The drink also contains rum, corn syrup and vanilla bean.', GETDATE(), GETDATE(), @User, @User, 13.5, @ml, 'http://www.31dover.com/media/catalog/product/cache/1/image/1000x1000/9df78eab33525d08d6e5fb8d27136e95/3/1/31dover-kahlua-320x1000.png'),
('Baileys', 'Baileys Irish Cream is an Irish whiskey and cream based liqueur.', GETDATE(), GETDATE(), @User, @User, 13, @ml, 'https://uk.thebar.com/assets/en-gb/spirits/liqueurs/baileys_orange.png'),
('Cream', 'Cream is a dairy product composed of the higher-butterfat layer skimmed from the top of milk before homogenization.', GETDATE(), GETDATE(), @User, @User, 0, @ml, 'http://www.chick-fil-a.com/Ext/Happily-Handcrafted/images/milkshake-station/whipped-cream1.png'),
('Tequila', 'Tequila is a regional specific name for a distilled beverage made from the blue agave plant, primarily in the area surrounding the city of Tequila, 65 km (40 mi) northwest of Guadalajara, and in the highlands (Los Altos) of the north western Mexican state of Jalisco. ', GETDATE(), GETDATE(), @User, @User, 54, @ml, 'http://www.officialpsds.com/images/thumbs/Tequila-Shot-psd100176.png'),
('Brandy', 'Brandy (from brandywine, derived from Dutch brandewijn, "gebrande wijn" "burned wine"[1]) is a spirit produced by distilling wine. Brandy generally contains 35–60% alcohol by volume (70–120 US proof) and is typically taken as an after-dinner drink.', GETDATE(), GETDATE(), @User, @User, 38, @ml, 'http://www.restaurantsource.com/resize/images/cardinal/C8071079.png?lr=t&bw=1000&w=1000&bh=1000&h=1000'),
('Schnapps', 'Schnapps is a "strong alcoholic drink resembling gin and often flavored with fruit.', GETDATE(), GETDATE(), @User, @User, 55, @ml, 'http://www.tirol-geniessen.com/media/images/detailtg2/williams_birne_schnaps_williams_pear_brandy_200.png'),
('Milk', 'Milk is a white liquid produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals before they are able to digest other types of food.', GETDATE(), GETDATE(), @User, @User, 0, @ml, 'http://www.deltaco.com/images/food/menu/drinks/milk.png')

/*Add Allergens to the ingredients*/
GO

Declare @dairy int 
set @dairy = (select Top 1 AllergenId from ALLERGEN where ShortName like '%Mil%')

Declare @gluten int 
set @gluten = (select Top 1 AllergenId from ALLERGEN where ShortName like '%Gluten%')

Declare @baileys int
set @baileys = (select Top 1 IngredientId from INGREDIENT where Name like '%Bai%')

Declare @kahlua int
set @kahlua = (select Top 1 IngredientId from INGREDIENT where Name like '%Kahl%')

Declare @whisky int
set @whisky = (select Top 1 IngredientId from INGREDIENT where Name like '%Whi%')

Declare @gin int
set @gin = (select Top 1 IngredientId from INGREDIENT where Name like '%Gin%')

Declare @vodka int
set @vodka = (select Top 1 IngredientId from INGREDIENT where Name like '%Vod%')

Declare @cream int
set @cream = (select Top 1 IngredientId from INGREDIENT where Name like '%Crea%')

INSERT INTO INGREDIENT_ALLERGEN VALUES
(1, @vodka, @gluten),
(1, @baileys, @dairy),
(1, @kahlua, @dairy),
(1, @whisky, @gluten),
(1, @gin, @gluten)

/*Add Nutritional info to the ingredients*/
INSERT INTO INGREDIENT_NUTRITIONAL_INFO VALUES
(327, @baileys, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Bre%')),
(3, @baileys, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Eiw%')),
(25, @baileys, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Kohl%')),
(13, @baileys, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Fet%')),
(0.08, @baileys, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Nat%')),
(0, @baileys, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Balla%'))

INSERT INTO INGREDIENT_NUTRITIONAL_INFO VALUES
(53, @kahlua, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Bre%')),
(0, @kahlua, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Eiw%')),
(11.3, @kahlua, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Kohl%')),
(0, @kahlua, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Fet%')),
(0, @kahlua, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Nat%')),
(0, @kahlua, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Balla%'))

INSERT INTO INGREDIENT_NUTRITIONAL_INFO VALUES
(345, @cream, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Bre%')),
(2, @cream, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Eiw%')),
(3, @cream, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Kohl%')),
(37, @cream, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Fet%')),
(0.038, @cream, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Nat%')),
(0, @cream, (select Nutritional_InfoId from NUTRITIONAL_INFO where ShortName like 'Balla%'))

GO

select * from RECIPE
