USE reserve_it;
DELIMITER //

CREATE PROCEDURE submitReview(IN auftrag_id_in INT, IN rezension_in NVARCHAR(500))
BEGIN

	DECLARE auftrag_id_vorhanden int;
	DECLARE auftrag_id_gueltig INT;
	DECLARE rezension_vorhanden NVARCHAR(500);
	
	SELECT COUNT(*) INTO auftrag_id_vorhanden
	FROM bewertung
	WHERE auftrag_ID = auftrag_id_in;
	
	SELECT COUNT(*) INTO auftrag_id_gueltig
	FROM auftrag
	WHERE auftrag_ID = auftrag_id_in;
	
	SELECT CHAR_LENGTH(rezension_in) INTO rezension_vorhanden;
	
	if auftrag_id_vorhanden = 0 AND auftrag_id_gueltig > 0 THEN
	
		INSERT INTO bewertung(auftrag_ID, istFreigegeben, rezension)
		values
		(auftrag_id_in, 'false', rezension_in);
		
		SELECT 1 AS orderIdResult;
		
	END if;
	
	if auftrag_id_vorhanden > 0 OR auftrag_id_gueltig = 0 THEN
		
		SELECT 0 AS orderIdResult;
		
	END if;
	
	if rezension_vorhanden > 0 THEN
	
		SELECT 1 AS reviewTextResult;
	
	END if;
	
	if rezension_vorhanden = 0 THEN
	
		SELECT 0 AS reviewTextResult;
	
	END if;
	
END//

DELIMITER ;_it;
/

EDURE submitReview(IN auftrag_id_in INT, IN rezension_in NVARCHAR(500))


ftrag_id_vorhanden int;
ftrag_id_gueltig INT;

NT(*) INTO auftrag_id_vorhanden
tung
rag_ID = auftrag_id_in;

NT(*) INTO auftrag_id_gueltig
ag
rag_ID = auftrag_id_in;

_id_vorhanden = 0 AND auftrag_id_gueltig > 0 THEN

TO bewertung(auftrag_ID, istFreigegeben, rezension)

id_in, 'false', rezension_in);

AS Result;



_id_vorhanden > 0 OR auftrag_id_gueltig = 0 THEN

AS Result;





