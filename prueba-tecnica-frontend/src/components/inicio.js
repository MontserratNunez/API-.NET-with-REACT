import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate, Link } from 'react-router-dom';

const PaginaInicio = () => {
  const [contribuyente, setContribuyente] = useState(null);
  const [rncCedula, setRncCedula] = useState('');
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const fetchContribuyente = async (event) => {
    event.preventDefault();
    setLoading(true);
    setError(null);

    try {
      const response = await axios.get(`http://localhost:5298/api/contribuyente/${rncCedula}`);
      setContribuyente(response.data.data.contribuyente);
    } catch (err) {
      setError('Error al cargar la información del contribuyente');
    } finally {
      setLoading(false);
    }
  };

  const handleContribuyenteClick = (rncCedula) => {
    navigate(`/${rncCedula}/comprobantes`);
  };

  if (loading) {
    return <div>Cargando contribuyente...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  return (
    <div>
      <h1>Página de inicio</h1>
      <ul className='center med g30 big'>
        <li><Link to="/lista-contribuyentes" className='grey'>Listado de contribuyentes</Link></li>
        <li><Link to="/add-contribuyente" className='grey'>Agregar contribuyente</Link></li>
        <li><Link to="/add-comprobante" className='grey'>Agregar comprobante</Link></li>
      </ul>
      <h4>Buscar contribuyente por RNC/Cédula</h4>
      <form onSubmit={fetchContribuyente}>
        <input placeholder='RNC/Cédula' value={rncCedula} onChange={(e) => setRncCedula(e.target.value)} required/>
        <button className='btn-1' type="submit">Buscar</button>
      </form><br/>
      {contribuyente && (
        <div>
          <li className='card'>
            <p className='big'>RNC/Cédula: {contribuyente.rncCedula}</p>
            <p className='med'>{contribuyente.name}</p>
            <div className='center g30'>
                <p className='litle'>Tipo: {contribuyente.type == "PERSONA FISICA" ? "PERSONA FÍSICA" : "PERSONA JURÍDICA"}</p> 
                <p className='litle'>Estatus: {contribuyente.status}</p>
                <button className='btn-2' onClick={() => handleContribuyenteClick(contribuyente.rncCedula)}>Detalles</button>
            </div>
            </li>
        </div>
      )}
    </div>
  );
};

export default PaginaInicio;