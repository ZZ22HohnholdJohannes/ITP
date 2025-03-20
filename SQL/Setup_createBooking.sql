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
   
	IF gast_id IS NULL THEN´                                # If the variable with the guest is NULL then the new guest will be added to the table anschrift
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