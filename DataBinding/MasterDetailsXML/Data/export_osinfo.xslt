<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <!--xsl:output method="xml" indent="yes"/-->
  <xsl:key name="osinfo-by-osname" match="tbl_osinfo" use="surname"/>

  <xsl:template match="@* | node()">
    <xsl:for-each select="tbl_osinfo[count(. | key('osinfo-by-osname', os_name)[1]) = 1]">
      <xsl:sort select="os_name"/>
      <xsl:value-of select="os_name"/>,<br />
      <xsl:for-each select="key('osinfo-by-osname', os_name)">
        <xsl:sort select="os_version"/>
        <xsl:value-of select="os_version"/>
        <br />
      </xsl:for-each>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>