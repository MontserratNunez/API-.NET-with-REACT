import React, { useState } from 'react';
import axios from 'axios';

const AddContribuyente = () => {
  const [name, setName] = useState('');
  const [type, setType] = useState('PERSONA FISICA');
  const [rncCedula, setRncCedula] = useState('');
  const [status, setStatus] = useState('ACTIVO');
  const [message, setMessage] = useState('');

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const response = await axios.post('http://localhost:5298/api/contribuyente/addContribuyente', {
        name,
        type,
        rncCedula,
        status
      });
      setMessage(response.data.message);
      console.log(message)
    } catch (error) {
      if (error.response && error.response.data) {
        setMessage(error.response.data.message);
      } else {
        setMessage('Error desconocido');
      }
    }
  };

  return (
    <div>
      <h2>Agregar Contribuyente</h2>
      {message && <div className="message center g30">{message}</div>}
      <div className='form'>
        <form className='form-el' onSubmit={handleSubmit}>
          <h4>Datos del nuevo contribuyente</h4>
          <div>
            <label className='med big'>Nombre:</label><br/>
            <input value={name} onChange={(e) => setName(e.target.value)} required/>
          </div>
          <div>
            <label className='med big'>Tipo:</label><br/>
            <select value={type} onChange={(e) => setType(e.target.value)} required>
              <option value="PERSONA FISICA">Persona Física</option>
              <option value="PERSONA JURIDICA">Persona Jurídica</option>
            </select>
          </div>
          <div>
            <label className='med big'>RNC/Cédula:</label><br/>
            <input value={rncCedula} onChange={(e) => setRncCedula(e.target.value)} required/>
          </div>
          <div>
            <label className='med big'>Estatus:</label><br/>
            <select value={status} onChange={(e) => setStatus(e.target.value)} required>
              <option value="ACTIVO">Activo</option>
              <option value="INACTIVO">Inactivo</option>
            </select>
          </div>
          <button className="btn-2" type="submit">Agregar Contribuyente</button>
        </form>
      </div>
    </div>
  );
};

export default AddContribuyente;