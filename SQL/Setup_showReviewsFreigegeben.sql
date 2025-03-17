USE reserve_it;

DELIMITER //
CREATE PROCEDURE showReviewsFreigegeben()
BEGIN 
	
	SELECT *
	FROM bewertung
	WHERE istFreigegeben = 1;
	
END//
DELIMITER ;