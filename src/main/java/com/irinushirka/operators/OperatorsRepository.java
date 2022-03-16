package com.irinushirka.operators;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
@Repository
public interface OperatorsRepository extends JpaRepository<Operators, Integer> {
}