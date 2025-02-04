import React from "react";

// Skicka projekten som en prop från App.js
const ProjectList = ({ projects }) => {
    return (
        <div>
            <h2>Projektlista</h2>
            <ul>
                {projects.length > 0 ? (
                    // Här använder vi .map för att rendera alla projekt
                    projects.map(project => (
                        <li key={project.id}>
                            {project.title ? project.title : "Ingen titel"} - {project.status ? project.status : "Ingen status"}
                        </li>
                    ))
                ) : (
                    // Om inga projekt finns, visa ett meddelande
                    <li>Inga projekt att visa</li>
                )}
            </ul>
        </div>
    );
};

export default ProjectList;


// Hela koden är IA genererad