import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Box from '@mui/material/Box';

function a11yProps(index) {
    return {
      'aria-controls': `simple-tabpanel-${index}`,
    };
}

const HeaderTabs = (props) => {
    return <Box>
    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
      <Tabs centered value={props.value} onChange={props.handleChange}>
        {props.tabs.map((t, i) => 
          <Tab key={i} label={t} {...a11yProps(i)} />
        )}
      </Tabs>
    </Box>
  </Box>
}

export default HeaderTabs;