USE buildings;

CREATE TABLE IF NOT EXISTS Streets (
  ID BIGINT PRIMARY KEY NOT NULL AUTO_INCREMENT,
  Name VARCHAR(100),
  Postal_code INT
);

CREATE TABLE IF NOT EXISTS Buildings (
  ID BIGINT PRIMARY KEY NOT NULL AUTO_INCREMENT,
  Name VARCHAR(100),
  Building_date DATE,
  Floors_amount INT,
  Building_type VARCHAR(100) NOT NULL CHECK (Building_type in ('Residential', 'Commercial', 'Garage', 'Utility')),
  Street BIGINT,
  FOREIGN KEY (Street) REFERENCES Streets(ID)
);