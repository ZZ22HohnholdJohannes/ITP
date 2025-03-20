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