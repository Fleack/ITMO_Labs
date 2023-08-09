package tools;

import entities.Building;
import entities.Street;
import hibernate.HibernateConnectionBuildingTable;
import hibernate.HibernateConnectionStreetTable;
import interfaces.BuildingTable;
import interfaces.StreetTable;
import jdbc.JdbcConnectionBuildingTable;
import jdbc.JdbcConnectionStreetTable;
import mybatis.BatisConnectionBuildingTable;
import mybatis.BatisConnectionStreetTable;
import mybatis.BuilderMapper;
import mybatis.StreetMapper;
import org.apache.ibatis.io.Resources;
import org.apache.ibatis.session.SqlSession;
import org.apache.ibatis.session.SqlSessionFactory;
import org.apache.ibatis.session.SqlSessionFactoryBuilder;

import java.io.IOException;
import java.io.InputStream;
import java.sql.Date;
import java.sql.SQLException;
import java.util.ArrayList;

public class Main {
    public static long testInsertStreetIntoTable(StreetTable scon) throws SQLException, DatabaseException {
        long startTime = System.currentTimeMillis();
        for (int i = 0; i < 100; ++i) {
            scon.save(new Street("Street", 1));
        }
        long endTime = System.currentTimeMillis();
        return endTime - startTime;
    }

    public static long testSelectStreetIntoTable(StreetTable scon) throws SQLException, DatabaseException {
        long startTime = System.currentTimeMillis();
        scon.getAll();
        long endTime = System.currentTimeMillis();
        return endTime - startTime;
    }

    public static void testInsert() throws SQLException, DatabaseException, IOException {
        // JDBC
        String url = "jdbc:mysql://localhost/buildings";
        String username = "root";
        String password = "j66iq0ig";
        JdbcConnectionStreetTable jdbcscon = new JdbcConnectionStreetTable(url, username, password);
        long jdbcDuration = testInsertStreetIntoTable(jdbcscon);
        jdbcscon.deleteAll();

        // Hibernate
        HibernateConnectionStreetTable hiberScon = new HibernateConnectionStreetTable();
        long hiberDuration = testInsertStreetIntoTable(hiberScon);
        hiberScon.deleteAll();

        // MyBatis
        String cfg = "mybatis-config.xml";
        BatisConnectionStreetTable batisScon = new BatisConnectionStreetTable(cfg);
        long batisDuration = testInsertStreetIntoTable(batisScon);

        System.out.println("JDBC: " + jdbcDuration + " ms");
        System.out.println("Hibernate: " + hiberDuration + " ms");
        System.out.println("MyBatis: " + batisDuration + " ms");
    }

    public static void testSelect() throws SQLException, DatabaseException, IOException {
        // JDBC
        String url = "jdbc:mysql://localhost/buildings";
        String username = "root";
        String password = "j66iq0ig";
        JdbcConnectionStreetTable jdbcscon = new JdbcConnectionStreetTable(url, username, password);
        long jdbcDuration = testSelectStreetIntoTable(jdbcscon);

        // Hibernate
        HibernateConnectionStreetTable hiberScon = new HibernateConnectionStreetTable();
        long hiberDuration = testSelectStreetIntoTable(hiberScon);

        // MyBatis
        String cfg = "mybatis-config.xml";
        BatisConnectionStreetTable batisScon = new BatisConnectionStreetTable(cfg);
        long batisDuration = testSelectStreetIntoTable(batisScon);

        System.out.println("JDBC: " + jdbcDuration + " ms");
        System.out.println("Hibernate: " + hiberDuration + " ms");
        System.out.println("MyBatis: " + batisDuration + " ms");
    }

    public static void main(String[] args) throws SQLException, DatabaseException, IOException {
        testInsert();
        System.out.println();
        testSelect();
    }
}
