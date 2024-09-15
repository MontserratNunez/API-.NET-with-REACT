import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const ContribuyentesLista = () => {
    const [contribuyentes, setContribuyentes] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [filterName, setFilterName] = useState('');
    const [filterType, setFilterType] = useState('');
    const [filterStatus, setFilterStatus] = useState('');
    const navigate = useNavigate();

    const fetchContribuyentes = async () => {
        try {
            const params = new URLSearchParams();
            if (filterName) params.append('name', filterName);
            if (filterType) params.append('type', filterType);
            if (filterStatus) params.append('status', filterStatus);
        
            const response = await axios.get(`http://localhost:5298/api/contribuyente?${params.toString()}`);
            const { data } = response.data;
            setContribuyentes(Array.isArray(data) ? data : []);
            setLoading(false);
        } catch (err) {
            setError('Error al cargar los contribuyentes');
            setLoading(false);
        }
    };

    const handleContribuyenteClick = (rncCedula) => {
        navigate(`/${rncCedula}/comprobantes`);
    };

    const addContribuyente = () => {
        navigate(`/add-contribuyente`);
    };

    const AddComprobante = () => {
        navigate(`/add-comprobante`);
    };

    const handleFilterChange = () => {
        fetchContribuyentes();
    };

    useEffect(() => {
        fetchContribuyentes();
    }, [filterName, filterType, filterStatus]);

    if (loading) {
        return <div>Cargando contribuyentes...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
        <h1>Listado de Contribuyentes</h1>
        <button className="btn-1" onClick={() => addContribuyente()}>Nuevo Contribuyente</button>
        <button className='btn-1' onClick={() => AddComprobante()}>Nuevo Comprobante</button>
        <nav className='nav-bar center'>
            <p>Filtrar por:</p>
            <input placeholder="Nombre" value={filterName} onChange={(e) => setFilterName(e.target.value)}></input>
            <select value={filterType} onChange={(e) => setFilterType(e.target.value)}>
                <option value="">Tipo</option>
                <option value="persona fisica">Persona física</option>
                <option value="persona juridica">Persona jurídica</option>
            </select>
            <select value={filterStatus} onChange={(e) => setFilterStatus(e.target.value)}>
                <option value="">Estatus</option>
                <option value="activo">Activo</option>
                <option value="inactivo">Inactivo</option>
            </select>
            <button className='btn-1' onClick={() => { setFilterName(''); setFilterType(''); setFilterStatus(''); }}>Limpiar filtros</button>
            </nav>
        <ul>
            {contribuyentes.map((contribuyente) => (
            <li className='card'>
                <p className='big'>RNC/Cédula: {contribuyente.rncCedula}</p>
                <p className='med'>{contribuyente.name}</p>
                <div className='center g30'>
                    <p className='litle'>Tipo: {contribuyente.type == "PERSONA FISICA" ? "PERSONA FÍSICA" : "PERSONA JURÍDICA"}</p> 
                    <p className='litle'>Estatus: {contribuyente.status}</p>
                    <button className='btn-2' onClick={() => handleContribuyenteClick(contribuyente.rncCedula)}>Detalles</button>
                </div>
            </li>
            ))}
        </ul>
        </div>
    );
};

export default ContribuyentesLista;