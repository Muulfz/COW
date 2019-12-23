print('COW: Loading LUA Backend...')

function OnTest(data)
    return 'Test'
end






print('COW: LUA Backend loaded!')
ExecuteNET('finish-wrapper', '');
print('COW: Trigger Wrapper to finish!')