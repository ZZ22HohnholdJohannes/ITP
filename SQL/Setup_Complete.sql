DROP DATABASE if EXISTS reserve_it;
CREATE DATABASE reserve_it;
USE reserve_it;

-- CREATE TABLES --

CREATE TABLE anschrift
(
	anschrift_ID INT auto_increment PRIMARY KEY 
,	strasse NVARCHAR(200)
,	hausnummer NVARCHAR(10)
,	ort NVARCHAR(200)
,	postleitzahl NVARCHAR(5)
,	land NVARCHAR(200)
);

CREATE TABLE gast
(
	gast_ID INT auto_increment PRIMARY KEY
,  anschrift_ID INT
,	vorname NVARCHAR(200)
,	nachname NVARCHAR(200)
,	geburtsdatum DATE 
,	istStammgast BOOL
,  geschlecht NVARCHAR(1) 
, 	FOREIGN KEY (anschrift_ID) REFERENCES anschrift(anschrift_ID)
);

CREATE TABLE auftrag
(
	auftrag_ID INT auto_increment PRIMARY KEY 
,  gast_ID int
, 	FOREIGN KEY (gast_ID) REFERENCES gast(gast_ID)
, 	startdatum DATE
, 	enddatum DATE 
);

CREATE TABLE hotel
(
	hotel_ID INT auto_increment PRIMARY KEY
,  anschrift_ID int
,	FOREIGN KEY (anschrift_ID) REFERENCES anschrift(anschrift_ID)
);

CREATE TABLE bewertung
(
	bewertung_ID INT auto_increment PRIMARY KEY 
,  auftrag_ID INT 
, 	FOREIGN KEY (auftrag_ID) REFERENCES auftrag(auftrag_ID)
, 	istFreigegeben BOOL
,  rezension NVARCHAR(500)
);

CREATE TABLE art
(
	art_ID INT PRIMARY KEY
,	art_beschreibung NVARCHAR(200)
);

CREATE TABLE kategorie
(
	kategorie_ID INT PRIMARY KEY 
,	kategorie_beschreibung NVARCHAR(200)
);

CREATE TABLE preis
(
	preis_ID INT PRIMARY KEY 
,  kategorie_ID INT
,  art_ID INT 
,	FOREIGN KEY (kategorie_ID) REFERENCES kategorie(kategorie_ID)
,	FOREIGN KEY (art_ID) REFERENCES art(art_ID)
,	preis_num INT
);

CREATE TABLE hotelzimmer
(
	hotelzimmer_ID INT auto_increment PRIMARY KEY 
,  preis_ID INT
,  hotel_ID INT
,  FOREIGN KEY (preis_ID) REFERENCES preis(preis_ID)
,  FOREIGN KEY (hotel_ID) REFERENCES hotel(hotel_ID)
,  zimmernummer INT
);



CREATE TABLE buchung
(
	buchung_ID INT auto_increment PRIMARY KEY 
,  auftrag_ID INT
,  hotelzimmer_ID INT
,	FOREIGN KEY (auftrag_ID) REFERENCES auftrag(auftrag_ID)
,	FOREIGN KEY (hotelzimmer_ID) REFERENCES hotelzimmer(hotelzimmer_ID)
);


-- ENTER DATA INTO TABLES --

# Setup_dummyDataTables

USE reserve_it;

INSERT INTO anschrift (strasse, hausnummer, ort, postleitzahl, land)
VALUES
('Hauptstraße', '10', 'Berlin', '10115', 'Deutschland'),
('Musterweg', '5', 'Hamburg', '20095', 'Deutschland'),
('Parkstraße', '14', 'München', '80331', 'Deutschland'),
('Bergstraße', '7', 'Köln', '50733', 'Deutschland'),
('Seestraße', '21', 'Berlin', '13407', 'Deutschland');

INSERT INTO gast (vorname, nachname, geburtsdatum, geschlecht, anschrift_ID, iststammgast)
VALUES
('Max', 'Mustermann', '1985-06-15', 'M', 1, 1),
('Erika', 'Musterfrau', '1990-09-20', 'W', 2, 0),
('Luca', 'Müller', '1995-03-25', 'M', 3, 0),
('Anna', 'Schmidt', '1988-07-30', 'W', 4, 1),
('Paul', 'Weber', '1992-11-12', 'M', 5, 0);

INSERT INTO hotel (anschrift_ID)
VALUES
(1),
(2),
(3),
(4),
(5);

INSERT INTO kategorie (kategorie_ID, kategorie_beschreibung)
VALUES
(1, 'Standard'),
(2, 'Premium'),
(3, 'Luxus');

INSERT INTO art (art_ID, art_beschreibung)
VALUES
(1, 'Einzelzimmer'),
(2, 'Doppelzimmer');

INSERT INTO preis (preis_ID, kategorie_ID, art_ID, preis_num)
VALUES
(1, 1, 1, 80.00),
(2, 1, 2, 120.00),
(3, 2, 1, 180.00),
(4, 2, 2, 250.00),
(5, 3, 1, 500.00),
(6, 3, 2, 600.00);

INSERT INTO hotelzimmer (preis_ID, hotel_ID, zimmernummer)
VALUES
(1, 1, '101'),
(2, 1, '102'),
(3, 2, '201'),
(4, 3, '301'),
(5, 4, '401'),
(6, 4, '403');

INSERT INTO auftrag (gast_ID, startdatum, enddatum)
VALUES
(1, '2024-03-01', '2024-03-05'),
(2, '2024-03-10', '2024-03-15'),
(3, '2024-04-01', '2024-04-07'),
(4, '2024-05-01', '2024-05-05'),
(5, '2024-06-01', '2024-06-05'),
(1, '2024-09-12', '2024-12-12'),
(2, '2024-10-01', '2024-10-11'),
(3, '2024-10-24', '2024-11-02');

INSERT INTO buchung (auftrag_ID, hotelzimmer_ID)
VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 1),
(7, 2),
(8, 3);

INSERT INTO bewertung (auftrag_ID, istfreigegeben, rezension)
VALUES
(1, 1, 'Super Aufenthalt!'),
(2, 0, 'Sehr gut, aber kleines Bad.'),
(3, 1, 'Alles war perfekt, sehr zufrieden!'),
(4, 1, 'Gutes Hotel, jedoch etwas zu teuer.'),
(5, 0, 'Nett, aber die Lage ist nicht ideal.');


-- CREATE SPs --

# Setup_checkAvailability

USE reserve_it;

DELIMITER //
CREATE PROCEDURE checkAvailability(IN startDate date, IN endDate DATE, IN kategorieZimmer INT, IN artZimmer INT) 
BEGIN

# Returns the first room matching the category and type the user entered if it is available for the duration of the stay

    SELECT k.kategorie_beschreibung AS kategorie,
           a.art_beschreibung AS zimmerart,
           p.preis_num AS preis_pro_nacht
    FROM hotelzimmer hz
    LEFT JOIN preis p ON hz.preis_ID = p.preis_ID
    LEFT JOIN art a ON p.art_ID = a.art_ID
    LEFT JOIN kategorie k ON k.kategorie_ID = p.kategorie_ID
    WHERE k.kategorie_ID = kategorieZimmer
      AND a.art_ID = artZimmer
      AND NOT EXISTS (
          SELECT 1 
          FROM buchung b
          JOIN auftrag auf ON b.auftrag_ID = auf.auftrag_ID
          WHERE b.hotelzimmer_ID = hz.hotelzimmer_ID
            AND NOT (auf.enddatum <= startDate OR endDate <= auf.startdatum)
      )
    LIMIT 1;
	       
END //
DELIMITER ;

#Setup createBooking

USE reserve_it;

DELIMITER //
CREATE PROCEDURE createBooking(IN geschlecht_in NVARCHAR(1)
									 	,IN vorname_in NVARCHAR(200)
									 	,IN nachname_in NVARCHAR(200)
									 	,IN geburtsdatum_in date
									 	,IN straße_in NVARCHAR(200)
									 	,IN hausnummer_in NVARCHAR(200)
									 	,IN plz_in NVARCHAR(5)
									 	,IN ort_in NVARCHAR(200)
									 	,IN land_in NVARCHAR(200)
									 	,IN startdatum_in date
									 	,IN enddatum_in date
									 	,IN kategorie_in int
									 	,IN art_in int)
BEGIN

# Creates new entries in the tables anschrift (if it doesn't exist) gast (if it doesn't exist) buchung and auftrag

	DECLARE gast_id INT;
	DECLARE anschrift_id INT;
	DECLARE zimmer_id INT;
	DECLARE auftrag_id INT;
   DECLARE gast_count INT;
	
	SELECT a.anschrift_id INTO anschrift_id						# ID of the address that was put in by the user is saved into a variable (doesn't exist = NULL entry)
	FROM anschrift a
	WHERE a.strasse = straße_in
	AND a.hausnummer = hausnummer_in
	AND a.ort = ort_in
	AND a.postleitzahl = plz_in
	AND a.land = land_in;
	
	SELECT g.gast_id INTO gast_id										# ID of the guest that made the booking is saved into a variable (no address/no guest = NULL entry)
	FROM gast g
	WHERE g.anschrift_ID = anschrift_id
	AND g.vorname = vorname_in
	AND g.nachname = nachname_in
	AND g.geschlecht = geschlecht_in;
	
	SELECT hz.hotelzimmer_ID INTO zimmer_id					  # ID of the room the guest wants to book is saved into variable using the same query as checkAvailability
	FROM hotelzimmer hz
  	left JOIN buchung b ON hz.hotelzimmer_ID = b.hotelzimmer_ID
  	left JOIN auftrag auf ON b.auftrag_ID = auf.auftrag_ID
  	left JOIN preis p ON hz.preis_ID = p.preis_ID
  	left JOIN art a ON p.art_ID = a.art_ID
  	left JOIN kategorie k ON k.kategorie_ID = p.kategorie_ID
  	WHERE (b.buchung_ID IS NULL OR  (auf.enddatum < startdatum_in OR enddatum_in < auf.startdatum)) 
     		AND k.kategorie_ID = kategorie_in
   		AND a.art_ID = art_in
	LIMIT 1;  
   
   IF anschrift_id IS NULL THEN                            # If the variable with the address is NULL then the new address will be added to the table anschrift
      INSERT INTO anschrift (strasse, hausnummer, ort, postleitzahl, land)
      VALUES (straße_in, hausnummer_in, ort_in, plz_in, land_in);
      SET anschrift_id = LAST_INSERT_ID();
   END IF;
   
	IF gast_id IS NULL THEN                                 # If the variable with the guest is NULL then the new guest will be added to the table anschrift
    	INSERT INTO gast (vorname, nachname, geburtsdatum, geschlecht, istStammgast, anschrift_ID)
    	VALUES (vorname_in, nachname_in, geburtsdatum_in, geschlecht_in, 0, anschrift_id);
    	SET gast_id = LAST_INSERT_ID();
	END IF;

   SELECT COUNT(*) INTO gast_count	                       # Counts the guests that are saved with the anschrift_ID saved in the variable
   FROM gast
   WHERE gast_ID = gast_id
     AND anschrift_ID = anschrift_id;

   IF gast_count = 0 THEN											  # If there are no guests that have that anschrift_ID then you update the one who made the booking
       UPDATE gast
       SET anschrift_ID = anschrift_id
       WHERE gast_ID = gast_id;
   END IF;
   
   INSERT INTO auftrag (gast_ID, startdatum, enddatum)	  # Creates a new entry for this booking in the table auftrag
   values
   (gast_id, startdatum_in, enddatum_in);
   
   SELECT auf.auftrag_id INTO auftrag_id							# ID of the new entry in the table auftrag is saved into a variable
   FROM auftrag auf
   WHERE auf.gast_ID = gast_id
   AND auf.startdatum = startdatum_in
   AND auf.enddatum = enddatum_in;
   
   INSERT INTO buchung (auftrag_ID, hotelzimmer_ID)			# With the saved auftrag_ID and hotelzimmer_ID a new entry in the table buchung is made
   values
   (auftrag_id, zimmer_id);

END//

DELIMITER ;


# Setup_deleteBooking

USE reserve_it;

DELIMITER //

CREATE PROCEDURE deleteBooking(IN auftrag_id_in INT)
BEGIN

# Deletes entry with the auftrag_ID of a specific order in the tables buchung, bewertung and auftrag
	
	DELETE FROM buchung 
	WHERE auftrag_ID = auftrag_id_in;
	
	DELETE FROM bewertung 
	WHERE auftrag_ID = auftrag_id_in;
	
	DELETE FROM auftrag 
	WHERE auftrag_ID = auftrag_id_in;
	
END//
DELIMITER ;

# Setup_reviewFreigeben

USE reserve_it;

DELIMITER //
CREATE PROCEDURE reviewFreigeben (IN bewertung_id_in INT)
BEGIN

	UPDATE bewertung
	SET istFreigegeben = 1
	WHERE bewertung_ID = bewertung_id_in;
	
END//
DELIMITER ;


# Setup_reviewLoeschen

USE reserve_it;

DELIMITER //
CREATE PROCEDURE reviewLoeschen (IN bewertung_id_in INT)
BEGIN

	DELETE
	FROM bewertung
	WHERE bewertung_ID = bewertung_id_in;

END//
DELIMITER ;

# Setup_showBookings

USE reserve_it

DELIMITER //

CREATE PROCEDURE showBookings(IN auftrag_id_in INT)
BEGIN 
	
# Returns values that are just used to display the owner of a booking in the AdminView

	SELECT g.vorname AS vorname
			,g.nachname AS nachname
			,a.startdatum AS startdatum
			,a.enddatum AS enddatum
			,ar.art_beschreibung AS zimmerart
			,k.kategorie_beschreibung AS kategorie
	FROM buchung b
	JOIN auftrag a ON b.auftrag_ID = a.auftrag_ID
	JOIN gast g ON g.gast_ID = a.gast_ID
	JOIN hotelzimmer hz ON hz.hotelzimmer_ID = b.hotelzimmer_ID
	JOIN preis p ON p.preis_ID = hz.preis_ID
	JOIN kategorie k ON k.kategorie_ID = p.kategorie_ID
	JOIN art ar ON ar.art_ID = p.art_ID
	WHERE a.auftrag_ID = auftrag_id_in;
	
END//
DELIMITER ;


# Setup_showReviewsFreigegeben

USE reserve_it;

DELIMITER //
CREATE PROCEDURE showReviewsFreigegeben()
BEGIN 

# User view of the ReviewView
	
	SELECT b.bewertung_ID AS bewertung_ID
			,a.auftrag_ID AS auftrag_ID
			,g.vorname AS Vorname
			,g.nachname AS Nachname
			,b.rezension AS rezension
	FROM bewertung b
	JOIN auftrag a ON a.auftrag_ID = b.auftrag_ID
	JOIN gast g ON g.gast_ID = a.gast_ID
	WHERE istFreigegeben = 1;
	
END//
DELIMITER ;


# Setup_showReviewsNichtFreigegeben

USE reserve_it

DELIMITER //
CREATE PROCEDURE showReviewsNichtFreigegeben()
BEGIN 
	
# Admin view of the ReviewView
	
	SELECT b.bewertung_ID AS bewertung_ID
			,a.auftrag_ID AS auftrag_ID
			,g.vorname AS Vorname
			,g.nachname AS Nachname
			,b.rezension AS rezension
	FROM bewertung b
	JOIN auftrag a ON a.auftrag_ID = b.auftrag_ID
	JOIN gast g ON g.gast_ID = a.gast_ID
	WHERE istFreigegeben = 0;
	
END//
DELIMITER ;


# Setup_submitReview

USE reserve_it;
DELIMITER //

CREATE PROCEDURE submitReview(IN auftrag_id_in INT, IN rezension_in NVARCHAR(500))
BEGIN

# Adds a new review to the table bewertung and returns 1 if successful

	DECLARE auftrag_id_vorhanden int;
	DECLARE auftrag_id_gueltig INT;
	
	SELECT COUNT(*) INTO auftrag_id_vorhanden                  # Counts how many reviews are in the table bewertung with the entered auftrag_ID and saves it into a variable
	FROM bewertung
	WHERE auftrag_ID = auftrag_id_in;
	
	SELECT COUNT(*) INTO auftrag_id_gueltig                    # Checks if the entered auftrag_ID exists in the table auftrag
	FROM auftrag
	WHERE auftrag_ID = auftrag_id_in;
	
	if auftrag_id_vorhanden = 0 AND auftrag_id_gueltig > 0 THEN      # If there's no reviews using the entered auftrag_ID
	                                                                 # And the auftrag_ID exists in the table auftrag
		INSERT INTO bewertung(auftrag_ID, istFreigegeben, rezension)  # A new review is added to bewertung with the entered values for auftrag_ID and rezension
		VALUES                                                        # The value istFreigegeben is set to false so it has to be cleared by a admin
		(auftrag_id_in, 'false', rezension_in);
		
		SELECT 1 AS Result;                                           # 1 is returned because the review was added successfully
		
	END if;
	
	if auftrag_id_vorhanden > 0 OR auftrag_id_gueltig = 0 THEN       # If one of the conditions from the first if-statement is not true
																						  # 0 is returned and the user gets a MessageBox
		SELECT 0 AS Result;
		
	END if;
	
END//

DELIMITER ;