USE reserve_it;
DELIMITER //

CREATE PROCEDURE submitReview(IN auftrag_id_in INT, IN bewertung_in NVARCHAR(500))
BEGIN

	DECLARE auftrag_id_vorhanden int;
	DECLARE auftrag_id_gueltig INT;
	
	SELECT COUNT(*) INTO auftrag_id_vorhanden
	FROM bewertung
	WHERE auftrag_ID = auftrag_id_in;
	
	SELECT COUNT(*) INTO auftrag_id_gueltig
	FROM auftrag
	WHERE auftrag_ID = auftrag_id_in;
	
	if auftrag_id_vorhanden = 0 AND auftrag_id_gueltig > 0 THEN
	
		INSERT INTO bewertung(auftrag_ID, istFreigegeben, rezension)
		values
		(auftrag_id_in, 'false', bewertung_in);
		
	END if;
	
	if auftrag_id_vorhanden > 0 OR auftrag_id_gueltig = 0 THEN
		
		SELECT 0 AS Result;
		
	END if;
	
END//

DELIMITER ;