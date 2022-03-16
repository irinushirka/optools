package com.irinushirka.document;



import com.irinushirka.users.UserNotFoundException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
public class DocumentControllers {

    @Autowired
    DocumentRepository docsRepository;

    // Get All Notes
    @GetMapping("/documents")
    public List<Documents> getAllNotes() {
        return docsRepository.findAll();
    }

    @PostMapping("/documents")
    public Documents createNote(@RequestBody Documents doc) {
        return docsRepository.save(doc);
    }

    @DeleteMapping("/documents/{id}")
    public Boolean deleteBook(@PathVariable(value = "id") Integer docId) throws UserNotFoundException {
        Documents book = docsRepository.findById(docId)
                .orElseThrow(() -> new UserNotFoundException(docId));

        docsRepository.delete(book);
        return true;//ResponseEntity.ok().build();
    }

    @PutMapping("/documents/{id}")
    public Documents updateNote(@PathVariable(value = "id") Integer bookId,
                                @RequestBody Documents docDetails) throws UserNotFoundException {

        Documents doc = docsRepository.findById(bookId)
                .orElseThrow(() -> new UserNotFoundException(bookId));

        doc.setClient(docDetails.getClient());
        doc.setDate_uploaded(docDetails.getDate_uploaded());
        doc.setDate_verified(docDetails.getDate_verified());
        doc.setDocument_id(docDetails.getDocument_id());
        doc.setDocument_issuer(docDetails.getDocument_issuer());
        doc.setDocument_number(docDetails.getDocument_number());
        doc.setDocumentType(docDetails.getDocumentType());
        doc.setExpiry_date(docDetails.getExpiry_date());


        Documents updatedDoc = docsRepository.save(doc);

        return updatedDoc;
    }
}