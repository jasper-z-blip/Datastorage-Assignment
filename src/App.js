import React, { useState, useEffect } from 'react';
import CreateProject from './components/CreateProject';
import ProjectList from './components/ProjectList';

function App() {
    const [projects, setProjects] = useState([]);

    const fetchProjects = async () => {
        try {
            const response = await fetch("http://localhost:5197/api/projects");
            const data = await response.json();
            console.log("Fetched Projects:", data);
            setProjects(data);
        } catch (error) {
            console.error("Fel vid hämtning av projekt:", error);
        }
    };

    useEffect(() => {
        fetchProjects();
    }, []);

    const addNewProject = (newProject) => {
        console.log("Ny projekt tillagd:", newProject);
        setProjects((prevProjects) => [...prevProjects, newProject]);
    };

    return (
        <div>
            <h1>Välkommen till Projektadministration</h1>
            <CreateProject refreshProjects={addNewProject} />
            {/* Skickar projektlistan som en prop till ProjectList */}
            <ProjectList projects={projects} />
        </div>
    );
}

export default App;

