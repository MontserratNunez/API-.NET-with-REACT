import React, { useState } from 'react';
import axios from 'axios';

const AddComprobante = () => {
  const [rncCedula, setRncCedula] = useState('');
  const [ncf, setNCF] = useState('');
  const [amount, setAmount] = useState('');
  const [message, setMessage] = useState('');

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const response = await axios.post('http://localhost:5298/api/contribuyente/AddComprobante', {
        rncCedula,
        ncf,
        amount,
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
      <h2>Nuevo Comprobante</h2>
      {message && <div className="message">{message}</div>}
      <div className='form'>
        <form className='form-el' onSubmit={handleSubmit}>
          <h4>Datos del nuevo comprobante</h4>
          <div>
            <label className='med big'>RNC/Cédula:</label><br/>
            <input value={rncCedula} required onChange={(e) => setRncCedula(e.target.value)}/>
          </div>
          <div>
            <label className='med big'>NCF:</label><br/>
            <input value={ncf} required onChange={(e) => setNCF(e.target.value)}/>
          </div>
          <div>
            <label className='med big'>Monto:</label><br/>
            <input value={amount} required onChange={(e) => setAmount(e.target.value)}/>
          </div>
          <button className="btn-2" type="submit">Nuevo Comprobante</button>
        </form>
      </div>
    </div>
  );
};

export default AddComprobante;