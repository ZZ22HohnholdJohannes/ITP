USE reserve_it;

DELIMITER //
CREATE PROCEDURE showReviewsFreigegeben()
BEGIN 
	
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