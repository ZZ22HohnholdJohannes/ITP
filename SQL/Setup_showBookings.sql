USE reserve_it

DELIMITER //

CREATE PROCEDURE showBookings(IN auftrag_id_in INT)
BEGIN 
	
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