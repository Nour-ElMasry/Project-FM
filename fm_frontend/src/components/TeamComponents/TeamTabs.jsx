import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Box from '@mui/material/Box';

function a11yProps(index) {
    return {
      id: `simple-tab-${index}`,
      'aria-controls': `simple-tabpanel-${index}`,
    };
  }


const TeamTabs = (props) => {
    
    return <Box>
    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
      <Tabs centered value={props.value} onChange={props.handleChange}>
        <Tab label="Matches" {...a11yProps(0)} />
        <Tab label="Results" {...a11yProps(1)} />
        <Tab label="Players" {...a11yProps(2)} />
      </Tabs>
    </Box>
  </Box>
}

export default TeamTabs;