package com.irinushirka.client;

import com.irinushirka.city.City;
import lombok.AllArgsConstructor;
        import lombok.Builder;
        import lombok.Data;
        import lombok.NoArgsConstructor;

        import javax.persistence.*;
        import java.io.Serializable;

@Entity
@Table(name = "clients")
@NoArgsConstructor
@AllArgsConstructor
@Data
@Builder
public class Client implements Serializable {
    @Id
    @GeneratedValue
    public Integer client_id;
    public String first_name;
    public String second_name;
    public String last_name;
    public String date_of_birth;

    @OneToOne(cascade = CascadeType.REFRESH)
    @JoinColumn(name = "city_code", referencedColumnName = "city_code")
    private City city;
}
