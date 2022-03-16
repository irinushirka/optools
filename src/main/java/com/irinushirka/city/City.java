package com.irinushirka.city;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.io.Serializable;

@Entity
@Table(name = "cities")
@NoArgsConstructor
@AllArgsConstructor
@Data
@Builder
public class City implements Serializable {
    @Id
    @GeneratedValue
    public Integer city_code;
    public String city;
}
