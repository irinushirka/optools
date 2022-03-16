package com.irinushirka.documentType;


import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.io.Serializable;

@Entity
@Table(name = "document_types")
@NoArgsConstructor
@AllArgsConstructor
@Data
@Builder
public class DocumentType implements Serializable  {
    @Id
    @GeneratedValue
    public Integer document_type_id;

    public String document_type;
}
