USE reserve_it;

DELIMITER //
CREATE PROCEDURE showReviewsNichtFreigegeben()
BEGIN 
	
	SELECT b.bewertung_ID
			,a.auftrag_ID
			,g.vorname
			,g.nachname
			,b.rezension
	FROM bewertung b
	JOIN auftrag a ON a.auftrag_ID = b.auftrag_ID
	JOIN gast g ON g.gast_ID = a.gast_ID
	WHERE istFreigegeben = 0;
	
END//
DELIMITER ;