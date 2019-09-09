import React, { Component } from 'react';



export class WorkItem extends Component {
    static displayName = WorkItem.name;

    constructor(props) {
        super(props);
        this.state = { workitems: [], loading: true };

        fetch('api/WorkItem/GetAll')
            .then(response => response.json())
            .then(data => {
                this.setState({ workitems: data, loading: false });
            });
    }

    static renderWorkItemTable(workitems) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Tipo</th>
                        <th>Título</th>
                        <th>Data de Criação</th>
                    </tr>
                </thead>
                <tbody>
                    {workitems.map(workitem =>
                        <tr key={workitem.id}>
                            <td>{workitem.id}</td>
                            <td>{workitem.type}</td>
                            <td>{workitem.title}</td>
                            <td>{workitem.createdOn}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Carregando...</em></p>
            : WorkItem.renderWorkItemTable(this.state.workitems);

        return (
            <div>
                <h1>Azure DevOps Work Items</h1>
                {contents}
            </div>
        );
    }
}