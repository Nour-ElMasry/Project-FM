import Tooltip from '@mui/material/Tooltip';
import IconButton from '@mui/material/IconButton';
import Toolbar from '@mui/material/Toolbar';
import FilterListIcon from '@mui/icons-material/FilterList';
import PlayreFilterDialog from './PlayerComponents/PlayerFilterDialog';
import { useState } from 'react';
import CancelIcon from '@mui/icons-material/Cancel';
import Button from '@mui/material/Button';

const FilterToolBar = (props) => {
    const [openFilter, setOpenFilter] = useState(false);
    const [filterOn, setFilterOn] = useState(false); 
    
    const handleClickOpen = () => {
        setOpenFilter(true);
    };
  
    const handleClose = () => {
        setOpenFilter(false);
    };

    const handleFilterReset = () => {
        setFilterOn(false);
        props.resetFilterData();
    }
 
    const handleFilterResetVisible = () => {
        setFilterOn(true);
    };
 
    return <>
    <PlayreFilterDialog 
        team={props.team}  
        openFilter={openFilter} 
        handleClose={handleClose} 
        handleFilterSubmit={props.handleFilterSubmit}
        handleFilterResetVisible={handleFilterResetVisible} 
    />

    <Toolbar className='flex flex-ai-c flex-jc-sb'>
        <h4>Players List</h4>
        <div>
            {filterOn && <Tooltip title="Remove Filter">
                <Button 
                    onClick={handleFilterReset} 
                    variant="contained" 
                    size='small'
                    color="error"
                    sx = {{marginRight:"0.75rem"}}
                    startIcon={<CancelIcon />}>
                    Filter
                </Button>
            </Tooltip>}
            <Tooltip title="Filter">
                <IconButton onClick={handleClickOpen}>
                    <FilterListIcon />
                </IconButton>
            </Tooltip>
        </div>
    </Toolbar>
</>
}

export default FilterToolBar;