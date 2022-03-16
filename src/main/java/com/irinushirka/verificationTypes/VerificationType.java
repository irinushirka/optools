package com.irinushirka.verificationTypes;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.io.Serializable;

@Entity
@Table(name = "verification_types")
@NoArgsConstructor
@AllArgsConstructor
@Data
@Builder
public class VerificationType implements Serializable  {
    @Id
    @GeneratedValue
    public Integer verification_type_id;

    public String verification_type;
}
