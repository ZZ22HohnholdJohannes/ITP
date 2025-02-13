USE reserve_it;

DELIMITER //

CREATE PROCEDURE showBookings(IN vorname_in NVARCHAR(200), IN nachname_in NVARCHAR(200), IN startDatum_in DATE, IN endDatum DATE, IN kategorie_in INT, IN art_in INT)
BEGIN

	SELECT a.auftrag_ID AS auftrag_ID
			,g.vorname AS vorname
			,g.nachname AS nachname
			,p.kategorie_ID AS kategorie_ID
			,p.art_ID AS art_ID
			,a.startdatum AS startdatum
			,a.enddatum AS enddatum
	FROM auftrag a
	JOIN gast g ON a.gast_ID = g.gast_ID
	JOIN buchung b ON a.auftrag_ID = b.auftrag_ID
	JOIN hotelzimmer h ON b.hotelzimmer_ID = h.hotelzimmer_ID
	JOIN preis p ON h.preis_ID = p.preis_ID
	WHERE (g.vorname = vorname_in OR vorname_in IS NULL)
	AND (g.nachname = nachname_in OR nachname_in IS NULL)
	AND (a.startdatum = startDatum_in OR startDatum_in IS NULL)
	AND (a.enddatum = endDatum_in OR endDatum_in IS NULL)
	AND (p.kategorie_ID = kategorie_in OR kategorie_in IS NULL)
	AND (p.art_ID = art_in OR art_in IS NULL);
	
END//

DELIMITER ;