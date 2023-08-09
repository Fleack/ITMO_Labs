package hibernate;

import entities.Building;
import entities.Street;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.boot.registry.StandardServiceRegistryBuilder;
import org.hibernate.cfg.Configuration;
import tools.DatabaseException;

public class HibernateConnection {
    private final SessionFactory sessionFactory;

    private Session session;

    public HibernateConnection() {
        Configuration configuration = new Configuration().configure();
        configuration.addAnnotatedClass(Building.class);
        configuration.addAnnotatedClass(Street.class);
        StandardServiceRegistryBuilder builder = new StandardServiceRegistryBuilder()
                .applySettings(configuration.getProperties());
        sessionFactory = configuration.buildSessionFactory(builder.build());
        session = null;
    }

    public Session getSession() {
        return session;
    }

    public void openConnection() throws DatabaseException {
        if (session != null && session.isOpen())
            return;

        try {
            session = sessionFactory.openSession();
        } catch (Exception e) {
            if (session != null && session.isOpen())
                session.close();
            throw new DatabaseException("Failed to openConnection");
        }
    }

    public void closeConnection() {
        if (session != null && session.isOpen())
            session.close();
    }
}
