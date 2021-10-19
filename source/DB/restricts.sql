ALTER TABLE Tour
ADD CONSTRAINT FK_TF FOREIGN KEY (Food) REFERENCES Food(FoodID),
ADD CONSTRAINT FK_TH FOREIGN KEY (Hotel) REFERENCES Hotel(HotelID),
ADD CONSTRAINT FK_TT FOREIGN KEY (Transfer) REFERENCES Transfer(TransferID);

ALTER TABLE Hotel 
ADD CONSTRAINT UK_H UNIQUE (Name);

ALTER TABLE Transfer
ADD CONSTRAINT FK_TP FOREIGN KEY (PlaneTicket) REFERENCES PlaneTicket(PlaneTID),
ADD CONSTRAINT FK_TT FOREIGN KEY (TrainTicket) REFERENCES TrainTicket(TrainTID),
ADD CONSTRAINT FK_TB FOREIGN KEY (BusTicket) REFERENCES BusTicket(BusTID);


/*CHECK*/
ALTER TABLE Food
ADD CONSTRAINT FCost_CHK CHECK (Cost >= 0),
ADD CONSTRAINT FCat_CHK CHECK (Category = 'Завтрак' OR Category = 'Полупансион' OR 
								Category = 'Полный пансион' OR Category = 'Полный пансион+' OR 
								Category = 'Все включено' OR Category = 'Континентальный завтрак' OR 
								Category = 'Английский завтрак' OR Category = 'Американский завтрак');

ALTER TABLE Tour 
ADD CONSTRAINT TCost_CHK CHECK (Cost >= 0),
ADD CONSTRAINT TDate_CHK CHECK (DateBegin <= DateEnd);

ALTER TABLE Users
ADD CONSTRAINT UAL_CHK CHECK (AccessLevel >= 0 AND AccessLevel <= 2);

ALTER TABLE Hotel 
ADD CONSTRAINT HCost_CHK CHECK (Cost >= 0),
ADD CONSTRAINT HType_CHK CHECK (Type = 'Отель' OR Type = 'Апартамент' OR 
								Type = 'Хостел' OR Type = 'Гостевой дом' OR 
								Type = 'Мотель' OR Type = 'Вила' OR 
								Type = 'Курортный отель' OR Type = 'Кемпинг' OR Type = 'Постель и завтрак'),
ADD CONSTRAINT HClass_CHK CHECK (Class >= 0 AND Class <= 5);

ALTER TABLE PlaneTicket
ADD CONSTRAINT PTCost_CHK CHECK (Cost >= 0),
ADD CONSTRAINT PTClass_CHK CHECK (Class = 1 OR Class = 2);

ALTER TABLE TrainTicket
ADD CONSTRAINT TTCost_CHK CHECK (Cost >= 0),
ADD CONSTRAINT TTCoach_CHK CHECK (Coach >= 1 AND Coach <= 20), 
ADD CONSTRAINT TTTime_CHK CHECK (DepartureTime < ArrivalTime);

ALTER TABLE BusTicket
ADD CONSTRAINT BTCost_CHK CHECK (Cost >= 0),
ADD CONSTRAINT TTTime_CHK CHECK (DepartureTime < ArrivalTime);





