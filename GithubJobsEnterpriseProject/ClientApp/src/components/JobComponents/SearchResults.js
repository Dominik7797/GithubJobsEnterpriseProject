import React, { useState } from 'react';
import axios from 'axios';
import Job from './Job'


export default function SearchResults() {

    const [searchResult, getSearch] = useState([]);

    const handleSubmit = (event) => {
        event.preventDefault()
        axios.get('api/description=' + event.target.descSearch.value
            + '&location=' +event.target.locSearch.value).then(jobData =>
            getSearch(jobData.data)
        )
    }
    return (
        <div>
             <form onSubmit={handleSubmit} style={{ padding: '5%', border: '1px solid #ced4da', marginBottom: '2%'}}>
                <div class="container search-form">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <input type="text" class="form-control" name='descSearch' placeholder="Python *"/>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <input type="text" class="form-control" name='locSearch' placeholder="Berlin *"/>
                            </div>
                        </div>
                    </div>
                </div>
                <button type="submit" style={{ border: 'none', borderRadius:'1.5rem',
                 padding: '1%',  width: '12%', cursor: 'pointer', background: '#0062cc', color: '#fff'}}>Search</button>
            </form>
            <p style={{fontSize:'20px'}}><strong>Search results:</strong></p>
            {searchResult.map(job => <Job job={job} />)}
        </div>
    )
}


