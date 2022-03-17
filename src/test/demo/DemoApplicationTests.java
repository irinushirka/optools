package com.irinushirka.demo;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.irinushirka.roles.Role;
import org.hamcrest.MatcherAssert;
import org.testng.annotations.Test;
import org.junit.jupiter.api.BeforeEach;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.context.ApplicationContext;
import org.springframework.http.MediaType;
import org.springframework.test.context.TestPropertySource;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.ResultActions;
import org.springframework.test.web.servlet.result.MockMvcResultHandlers;
import org.assertj.core.api.AssertionsForClassTypes;

import java.io.Serializable;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.util.Arrays;

import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@SpringBootTest
@AutoConfigureMockMvc
public class DemoApplicationTests {

	@Autowired
	private MockMvc mockMvc;

	@Autowired
	private ApplicationContext applicationContext;

	long id = 2L;

	@BeforeEach
	void printApplicationContext() {
		Arrays.stream(applicationContext.getBeanDefinitionNames())
				.map(name -> applicationContext.getBean(name).getClass().getName())
				.sorted()
				.forEach(System.out::println);
	}

	@Test
	public void testBlockUser() throws Exception {
		mockMvc.perform(get("/blockUser/" + id))
				.andDo(MockMvcResultHandlers.print())
				.andExpect(status().isOk());
	}

	@Test
	public void testGetUsers() throws Exception {
		mockMvc.perform(get("/users/" + id))
				.andDo(MockMvcResultHandlers.print())
				.andExpect(status().isOk());
	}

	@Autowired
	private TestRestTemplate restTemplate;


	@Test
	public void testLogin() {
		AssertionsForClassTypes
				.assertThat(this.restTemplate.postForEntity("http://localhost:" + 8080 + "/login", new LoginRequest("user", "user"),
						User.class));
	}

	@Test
	public void testRegisterUserAccount() {
		AssertionsForClassTypes
				.assertThat(this.restTemplate.postForEntity("http://localhost:" + 8080 + "/registration", new User(7, "irka", "irkaaa", "irkair@gmail.com", false, new Role()),
						User.class));
	}

	@Test
	public void testDeleteUser() throws Exception {
		mockMvc.perform(delete("/users/" + id))
				.andDo(MockMvcResultHandlers.print())
				.andExpect(status().isOk());
	}

	@Test
	public void testGetDocuments() throws Exception {
		ResultActions resultActions = mockMvc.perform(get("/documents/"))
				.andDo(MockMvcResultHandlers.print())
				.andExpect(status().isOk());
	}

	@Test
	public void addDocument() throws Exception {
		mockMvc.perform(post("/documents/").content(" {\n" +
				"        \"document_id\": 1,\n" +
				"        \"date_uploaded\": \"IRKA\",\n" +
				"        \"document_number\": \"DASDASDAS\",\n" +
				"        \"date_verified\": \"ASDASD\",\n" +
				"        \"expiry_date\": \"ASDASDAS\",\n" +
				"        \"document_issuer\": \"ASDASDAS\",\n" +
				"        \"documentType\": {\n" +
				"            \"document_type_id\": 1,\n" +
				"            \"document_type\": \"asdsadasdsa\"\n" +
				"        },\n" +
				"        \"status\": {\n" +
				"            \"status\": \"sadasdasdasdas\",\n" +
				"            \"status_id\": 1\n" +
				"        },\n" +
				"        \"client\": {\n" +
				"            \"client_id\": 1,\n" +
				"            \"first_name\": \"dasdasdas\",\n" +
				"            \"second_name\": \"asdsadasdasdasd\",\n" +
				"            \"last_name\": \"dasdasdasd\",\n" +
				"            \"date_of_birth\": \"asdsadas\",\n" +
				"            \"city\": {\n" +
				"                \"city_code\": 1,\n" +
				"                \"city\": \"asdasdasda\"\n" +
				"            }\n" +
				"        },\n" +
				"        \"operator\": {\n" +
				"            \"operator_id\": 1,\n" +
				"            \"first_name\": \"ASDASD\",\n" +
				"            \"second_name\": \"ASDASDASDSA\",\n" +
				"            \"last_name\": \"DASDASD\",\n" +
				"            \"accumulated_tenure\": \"DASD\",\n" +
				"            \"account\": {\n" +
				"                \"user_id\": 1,\n" +
				"                \"login\": \"ASDASD\",\n" +
				"                \"password\": \"ASDA\",\n" +
				"                \"email\": \"ASDASD\",\n" +
				"                \"is_blocked\": false,\n" +
				"                \"role\": null\n" +
				"            }\n" +
				"        },\n" +
				"        \"verificationType\": {\n" +
				"            \"verification_type_id\": 1,\n" +
				"            \"verification_type\": \"asdasdasdas\"\n" +
				"        }\n" +
				"    }"))
				.andDo(MockMvcResultHandlers.print())
				.andExpect(status().isOk());
	}

	@Test
	public void removeDocument() throws Exception {
		ResultActions resultActions = mockMvc.perform(delete("/documents/" + id))
				.andDo(MockMvcResultHandlers.print())
				.andExpect(status().isOk());
	}

	@Test
	public void editDoc() throws Exception {
		ResultActions resultActions = mockMvc.perform(put("/documents/" + id).content(" {\n" +
				"        \"document_id\": 1,\n" +
				"        \"date_uploaded\": \"IRKA\",\n" +
				"        \"document_number\": \"DASDASDAS\",\n" +
				"        \"date_verified\": \"ASDASD\",\n" +
				"        \"expiry_date\": \"ASDASDAS\",\n" +
				"        \"document_issuer\": \"ASDASDAS\",\n" +
				"        \"documentType\": {\n" +
				"            \"document_type_id\": 1,\n" +
				"            \"document_type\": \"asdsadasdsa\"\n" +
				"        },\n" +
				"        \"status\": {\n" +
				"            \"status\": \"sadasdasdasdas\",\n" +
				"            \"status_id\": 1\n" +
				"        },\n" +
				"        \"client\": {\n" +
				"            \"client_id\": 1,\n" +
				"            \"first_name\": \"dasdasdas\",\n" +
				"            \"second_name\": \"asdsadasdasdasd\",\n" +
				"            \"last_name\": \"dasdasdasd\",\n" +
				"            \"date_of_birth\": \"asdsadas\",\n" +
				"            \"city\": {\n" +
				"                \"city_code\": 1,\n" +
				"                \"city\": \"asdasdasda\"\n" +
				"            }\n" +
				"        },\n" +
				"        \"operator\": {\n" +
				"            \"operator_id\": 1,\n" +
				"            \"first_name\": \"ASDASD\",\n" +
				"            \"second_name\": \"ASDASDASDSA\",\n" +
				"            \"last_name\": \"DASDASD\",\n" +
				"            \"accumulated_tenure\": \"DASD\",\n" +
				"            \"account\": {\n" +
				"                \"user_id\": 1,\n" +
				"                \"login\": \"ASDASD\",\n" +
				"                \"password\": \"ASDA\",\n" +
				"                \"email\": \"ASDASD\",\n" +
				"                \"is_blocked\": false,\n" +
				"                \"role\": null\n" +
				"            }\n" +
				"        },\n" +
				"        \"verificationType\": {\n" +
				"            \"verification_type_id\": 1,\n" +
				"            \"verification_type\": \"asdasdasdas\"\n" +
				"        }\n" +
				"    }"))
				.andDo(MockMvcResultHandlers.print())
				.andExpect(status().isOk());
	}

	@Test
	public void testGetOperators() throws Exception {
		ResultActions resultActions = mockMvc.perform(get("/operators/"))
				.andDo(MockMvcResultHandlers.print())
				.andExpect(status().isOk());
	}

	@Test
	public void testChangeOperators() throws Exception {
		ResultActions resultActions = mockMvc.perform(get("/operators/" + id).content("    {\n" +
				"        \"operator_id\": 1,\n" +
				"        \"first_name\": \"ASDASD\",\n" +
				"        \"second_name\": \"ASDASDASDSA\",\n" +
				"        \"last_name\": \"DASDASD\",\n" +
				"        \"accumulated_tenure\": \"DASD\",\n" +
				"        \"account\": {\n" +
				"            \"user_id\": 1,\n" +
				"            \"login\": \"ASDASD\",\n" +
				"            \"password\": \"ASDA\",\n" +
				"            \"email\": \"ASDASD\",\n" +
				"            \"is_blocked\": false,\n" +
				"            \"role\": null\n" +
				"        }\n" +
				"    }"))
				.andDo(MockMvcResultHandlers.print())
				.andExpect(status().isOk());
	}

	@Test
	public void testDeleteOperator() throws Exception {
		ResultActions resultActions = mockMvc.perform(get("/operators/" + id))
				.andDo(MockMvcResultHandlers.print())
				.andExpect(status().isOk());
	}
}

class LoginRequest implements Serializable {
	private String login;
	private String password;

	public LoginRequest(String login, String password) {
		this.login = login;
		this.password = password;
	}
}

@SpringBootTest
@TestPropertySource(locations = "classpath:test.properties")
@Retention(RetentionPolicy.RUNTIME)
@interface MockMvcTest {}