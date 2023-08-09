USE buildings;
ALTER TABLE Buildings ADD COLUMN Material VARCHAR(100);

CREATE TABLE IF NOT EXISTS Apartments (
    ID BIGINT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    Number INT,
    Area INT,
    Rooms_amount INT,
    Building BIGINT,
    FOREIGN KEY (Building) REFERENCES Buildings(ID)
);