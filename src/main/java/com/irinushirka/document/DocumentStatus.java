package com.irinushirka.document;



import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.Table;
import java.io.Serializable;


@Entity
@Table(name = "document_status")
public class DocumentStatus implements Serializable {
    public String status;

    @Id
    @GeneratedValue
    public Integer status_id;
}