# RouterOS 6 и 7

Различия версий должны быть локализованы в проекте `MikroTikSetupWizard.RouterOs`.

Текущий минимальный сценарий использует команды, рассчитанные на RouterOS 6/7:

- `/system identity`
- `/interface list`
- `/interface bridge`
- `/ip address`
- `/ip pool`
- `/ip dhcp-server`
- `/ip dns`
- `/ip firewall nat`
- `/ip firewall filter`
- `/user`

Для будущих модулей нужно расширять:

- `RouterOsCapabilities`
- `RouterOsSyntaxPolicy`
- специализированные renderer/policy классы, если синтаксис RouterOS 6 и 7 расходится.
