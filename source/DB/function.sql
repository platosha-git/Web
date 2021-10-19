DROP FUNCTION IF EXISTS FullTour;
CREATE FUNCTION FullTour(TID int)
RETURNS TABLE
(
	TourID int,
	City VARCHAR(30),
	Name VARCHAR(30),
	Type VARCHAR (30),
	Category VARCHAR (30),
	Transfer INT,
	Cost INT,
	DateBegin DATE,
	DateEnd DATE
)
AS $$
	SELECT TourID, City, Name, Type, Category, Transfer, Tour.Cost, DateBegin, DateEnd 
	FROM Tour JOIN Food ON Tour.Food = Food.FoodID
		JOIN Hotel ON Tour.Hotel = Hotel.HotelID
	WHERE Tour.TourID = TID;
$$ LANGUAGE SQL;

select * from FullTour(2);